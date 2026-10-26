using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.Events;

public record TodoItemAdded(long TodoId, string name) : IDomainEvent;