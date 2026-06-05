using Scriban;
using Toarnbeike.Results.Guards.Generator.Rendering;
using Toarnbeike.Results.Guards.Generator.Semantics;

Console.WriteLine("Starting the primitive extensions generator");

var guardsProjectDirectory = "../../../../../src/Results.Guards";
var generatedPrimitiveExtensionsDirectory = Path.Combine(guardsProjectDirectory, "Extensions", "Generated");

var compilation = Semantic.LoadCompilation(guardsProjectDirectory);
var rules = Semantic.GetRules(compilation);

var methodTemplate = Template.Parse(File.ReadAllText("../../../Templates/PrimitiveMethod.scriban"));
var fileTemplate = Template.Parse(File.ReadAllText("../../../Templates/PrimitiveExtensionsFile.scriban"));

foreach (var file in new DirectoryInfo(generatedPrimitiveExtensionsDirectory).GetFiles())
{
    file.Delete();
}

foreach (var ruleGroup in rules.GroupBy(rule => rule.MethodClass))
{
    var groupKey = ruleGroup.Key;
    var renderedModels = ruleGroup
        //.OrderBy(rule => rule.MethodName)
        .Select(rule => PrimitiveExtensionRendering.RenderExtension(rule, methodTemplate))
        .ToList();

    var fileContent = PrimitiveExtensionRendering.RenderGroup(groupKey, renderedModels, fileTemplate);

    File.WriteAllText(Path.Combine(generatedPrimitiveExtensionsDirectory, $"{groupKey}PrimitiveExtensions.cs"), fileContent);
}