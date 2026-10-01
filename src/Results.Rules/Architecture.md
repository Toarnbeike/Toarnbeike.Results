Rule
- Rules zijn immutable.
- Rules zijn stateless.
- Runtime afhankelijkheden zitten in ValidationContext.
- Runtime evaluatiegegevens zitten in IRuleEvaluationData.
- Type refinement is een expliciet concept via IRefiningRule.
- Failure messages mogen evaluatiegegevens gebruiken.
- Condition messages zijn pure rule metadata.

ValidationContext
 ├─ CultureInfo
 ├─ TimeProvider
 └─ ToleranceProvider

Validator<T>
 ├─ mag DI gebruiken
 ├─ opgebouwd uit Rules
 ├─ produceert ValidationSummary
 └─ kan Conditions() genereren

DSL
 ├─ source generated
 ├─ façade boven Rules
 └─ compile-time type refinement support