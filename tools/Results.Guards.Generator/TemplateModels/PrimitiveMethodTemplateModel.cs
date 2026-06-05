using Toarnbeike.Results.Guards.Generator.TypeModels;

namespace Toarnbeike.Results.Guards.Generator.TemplateModels;

internal sealed record PrimitiveMethodTemplateModel(
    string MethodName,
    string TargetType,
    string TypeParameters,
    string ParameterList,
    string ArgumentList,
    IReadOnlyList<string> Constraints)
{
    public static PrimitiveMethodTemplateModel Create(RuleModel ruleModel)
    {
        return new PrimitiveMethodTemplateModel(
            MethodName: ruleModel.MethodName,
            TargetType: ruleModel.TargetType,

            TypeParameters:
            ruleModel.GenericParameters.Any()
                ? $"<{string.Join(", ",
                    ruleModel.GenericParameters.Select(x => x.Name))}>"
                : string.Empty,

            ParameterList:
            string.Join(", ",
                ruleModel.Parameters.Select(x => $"{x.Type} {x.Name}")),

            ArgumentList:
            string.Join(", ",
                ruleModel.Parameters.Select(x => x.Name)),

            Constraints:
            ruleModel.GenericParameters
                .Select(RenderConstraint)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList());
    }

    private static string RenderConstraint(GenericParameterModel parameterModel)
    {
        var constraints = new List<string>();

        if (parameterModel.HasStructConstraint)
            constraints.Add("struct");

        if (parameterModel.HasClassConstraint)
            constraints.Add("class");

        if (parameterModel.HasNotNullConstraint)
            constraints.Add("notnull");

        if (parameterModel.HasUnmanagedConstraint)
            constraints.Add("unmanaged");

        constraints.AddRange(parameterModel.TypeConstraints);

        return constraints.Count == 0
            ? string.Empty
            : $"where {parameterModel.Name} : {string.Join(", ", constraints)}";
    }
}