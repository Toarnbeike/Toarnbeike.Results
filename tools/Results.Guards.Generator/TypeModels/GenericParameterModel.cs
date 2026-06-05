namespace Toarnbeike.Results.Guards.Generator.TypeModels;

internal sealed record GenericParameterModel(
    string Name,
    bool HasStructConstraint,
    bool HasClassConstraint,
    bool HasNotNullConstraint,
    bool HasUnmanagedConstraint,
    IReadOnlyList<string> TypeConstraints);