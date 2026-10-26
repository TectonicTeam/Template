using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.Events;

public record TodoValueChanged(long Id, string Value) : IDomainEvent;