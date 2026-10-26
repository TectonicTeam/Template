using Dekacore.Domain;
using Ds.FleetManagement.Domain.ToDo;
using Ds.FleetManagement.Domain.ToDo.Events;
using MediatR;


namespace BC.TempateFull.Application.Command.UseCases.Todos;

public record NewMemberActivityRegistered(Todo todo) : IDomainEvent;
internal class NewMemberRegisteredHandler() : IDomainEventHandler<TodoCreated>
{
    public Task Handle(TodoCreated notification, CancellationToken cancellationToken)
    {

        Console.WriteLine("An Todo Created");

        return Task.CompletedTask;

    }
}
