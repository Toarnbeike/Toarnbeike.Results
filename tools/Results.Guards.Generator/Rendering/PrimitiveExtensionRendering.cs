using Scriban;
using Toarnbeike.Results.Guards.Generator.TemplateModels;
using Toarnbeike.Results.Guards.Generator.TypeModels;

namespace Toarnbeike.Results.Guards.Generator.Rendering;

internal static class PrimitiveExtensionRendering
{
    public static string RenderExtension(RuleModel rule, Template methodTemplate)
    {
        var renderModel = PrimitiveMethodTemplateModel.Create(rule);
        return methodTemplate.Render(new
        {
            method_name = renderModel.MethodName,
            target_type = renderModel.TargetType,
            type_parameters = renderModel.TypeParameters,
            constraints = renderModel.Constraints,
            argument_list = renderModel.ArgumentList,
            parameter_list = renderModel.ParameterList,
        });
    }

    public static string RenderGroup(string groupName, IEnumerable<string> methods, Template fileTemplate)
    {
        var fileContent = fileTemplate.Render(new
        {
            class_name = $"{groupName}PrimitiveExtensions", methods
        });

        return fileContent;
    }
}
