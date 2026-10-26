using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.Events;

public record TodoCreated(Todo todo) : IDomainEvent;