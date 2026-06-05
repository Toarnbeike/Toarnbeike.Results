namespace Toarnbeike.Results.Guards.Generator.TypeModels;

internal sealed record RuleModel(
    string Namespace,
    string MethodClass,
    string MethodName,
    string TargetType,
    IReadOnlyList<GenericParameterModel> GenericParameters,
    IReadOnlyList<RuleParameterModel> Parameters);