using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Toarnbeike.Results.Guards.Generator.TypeModels;

namespace Toarnbeike.Results.Guards.Generator.Semantics;

internal static class Semantic
{
    public static CSharpCompilation LoadCompilation(string projectPath)
    {
        var sourceFiles = Directory
            .EnumerateFiles(
                projectPath,
                "*.cs",
                SearchOption.AllDirectories)
            .ToList();

        var syntaxTrees = sourceFiles
            .Select(file =>
                CSharpSyntaxTree.ParseText(
                    File.ReadAllText(file),
                    path: file))
            .ToList();

        return CSharpCompilation.Create(
            "Analysis",
            syntaxTrees,
            references:
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            ]);
    }

    public static IEnumerable<RuleModel> GetRules(CSharpCompilation compilation)
    {
        return 
            from tree in compilation.SyntaxTrees 
            let model = compilation.GetSemanticModel(tree) 
            let methods = tree.GetMethods()

            from method in methods 
            let symbol = model.GetDeclaredSymbol(method) 
            where HasGenerateAttribute(symbol) 
            select ExtractRule(method, symbol, model);
    }

    private static IEnumerable<MethodDeclarationSyntax> GetMethods(this SyntaxTree tree) =>
        tree.GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>();

    private static bool HasGenerateAttribute([NotNullWhen(true)] IMethodSymbol? method)
    {
        return method?
            .GetAttributes()
            .Any(x =>
                x.AttributeClass?.Name ==
                "GeneratePrimitiveOverloadAttribute") ?? false;
    }

    private static RuleModel ExtractRule(MethodDeclarationSyntax syntax, IMethodSymbol symbol, SemanticModel model)
    {
        var extensionBlock = (ExtensionBlockDeclarationSyntax)syntax.Parent!;
        var receiverParameter = extensionBlock.ParameterList!.Parameters.Single();

        var receiverType = (INamedTypeSymbol)model.GetTypeInfo(receiverParameter.Type!).Type!;

        var guardedType = receiverType.TypeArguments.Single();

        var genericParameters =
            symbol.ContainingType
                .TypeParameters
                .Select(tp =>
                    new GenericParameterModel(
                        Name: tp.Name,
                        HasStructConstraint: tp.HasValueTypeConstraint,
                        HasClassConstraint: tp.HasReferenceTypeConstraint,
                        HasNotNullConstraint: tp.HasNotNullConstraint,
                        HasUnmanagedConstraint: tp.HasUnmanagedTypeConstraint,
                        TypeConstraints: tp.ConstraintTypes
                            .Select(x => x.ToDisplayString())
                            .ToList()))
                .ToList();

        return new RuleModel(
            Namespace: symbol.ContainingNamespace.ToDisplayString(),
            MethodClass: symbol.ContainingType.ContainingType.Name,
            MethodName: symbol.Name,
            TargetType: guardedType.ToDisplayString(),
            GenericParameters: genericParameters,
            Parameters: symbol.Parameters
                .Select(p =>
                    new RuleParameterModel(
                        p.Type.ToDisplayString(format: SymbolDisplayFormat.FullyQualifiedFormat),
                        p.Name))
                .ToList());
    }
}
