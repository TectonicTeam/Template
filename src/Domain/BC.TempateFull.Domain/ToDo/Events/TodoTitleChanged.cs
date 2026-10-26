using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.Events;

public record TodoTitleChanged(long Id, string Title) : IDomainEvent;
