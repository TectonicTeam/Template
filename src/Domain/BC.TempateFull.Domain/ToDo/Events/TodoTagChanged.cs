using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.Events;

public record TodoTagChanged(long Id, string Tag) : IDomainEvent;
