using Dekacore.Abstraction.IdGenerator;
using Dekacore.Extension.Mediator.Transaction;
using Dekacore.Messaging;
using Dekacore.Persistence;
using Ds.FleetManagement.Domain.ToDo;
using Ds.FleetManagement.Domain.ToDo.Enums;
using Ds.FleetManagement.Domain.ToDo.Services;
using BC.TempateFull.Application.Contracts.Events;
using MediatR;

namespace BC.TempateFull.Application.Command.UseCases.Todos;

public sealed record CreateAnTodoCommand(string Title, string Value, string Tag, TodoType OptionType) : ITransactionalRequest<long>;


internal class CreateAnTodoCommandHandler(
    IUnitOfWork unitOfWork,
    INumericIdGenerator idGenerator,
    IEventPublisher publisher,
    ITodoNameUsageValidator nameValidtor
    ) :
    IRequestHandler<CreateAnTodoCommand, long>
{
    public async Task<long> Handle(CreateAnTodoCommand request, CancellationToken token)
    {
        var repository = unitOfWork.GetRepository<ITodoRepository, Todo>();

        var id = idGenerator.Generate();
        var toto = await Todo.CreateTodoAsync(id, request.Title, request.Value, request.Tag, request.OptionType, nameValidtor);
        await repository.InsertAsync(toto, token);

        await publisher.PublishAsync(new TodoCreatedIntegrationEvent(id), token);
        return id;
    }
}

