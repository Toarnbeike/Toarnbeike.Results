# Request Messaging

De request-messaging component van `Toarnbeike.Results.Messaging` biedt een uniforme manier om queries en commands te definiëren, verwerken en uitbreiden.
Alle handlers leveren resultaten terug in de vorm van `Result` of `Result<T>`, zodat fouten en successen consistent afgehandeld kunnen worden.

---

## Request Types

Binnen Toarnbeike.Results.Messaging zijn er vier soorten requests, elk met een duidelijke verantwoordelijkheid:

- `IQuery<TResponse>`
Vraagt gegevens op zonder de toestand van het systeem te wijzigen.
Resultaat: `Result<TResponse>`

- `ICommand`
Wijzigt de toestand van het systeem, maar geeft zelf geen data terug.
Resultaat: `Result`

- `ICreateCommand<TCreated>`
Voert een wijziging uit waarbij een nieuwe entiteit wordt aangemaakt en teruggegeven.
Resultaat: `Result<TCreated>`

- `IPaginatedQuery<TResponse>`
Vraagt gegevens op met ondersteuning voor paginering.
Resultaat: `Result<PaginatedCollection<TResponse>>`

---
