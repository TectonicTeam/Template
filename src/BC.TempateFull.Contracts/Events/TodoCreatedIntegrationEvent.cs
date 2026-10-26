using Dekacore.Core.Abstraction;
using Dekacore.Messaging;

namespace BC.TempateFull.Application.Contracts.Events;

[Topic("Catalog.AttributeOption.Created")]
public record TodoCreatedIntegrationEvent(long Id) : IEvent;