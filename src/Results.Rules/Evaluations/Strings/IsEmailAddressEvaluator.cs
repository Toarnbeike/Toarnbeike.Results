using System.Net.Mail;

namespace Toarnbeike.Results.Rules.Evaluations.Strings;

/// <summary>
/// Checks if a given string is a valid email address.
/// </summary>
internal class IsEmailAddressEvaluator : IEvaluation<string>
{
    public EvaluationResult Evaluate(string value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: MailAddress.TryCreate(value, out var formatted),
            evaluatorName: "IsEmailAddressEvaluator",
            details: [("formatted", formatted!)]);
}