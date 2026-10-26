using Dekacore.Core.Exceptions;
using Dekacore.Extension.Mediator.Transaction;
using Ds.FleetManagement.Domain.ToDo;
using MediatR;

namespace BC.TempateFull.Application.Command.UseCases.Todos;

public record DeleteTodoCommand(long Id) : ITransactionalRequest<long>;

public class DeleteTodoCommandHandler(ITodoRepository repository)
    : IRequestHandler<DeleteTodoCommand, long>
{
    public async Task<long> Handle(DeleteTodoCommand request, CancellationToken token)
    {
        var result = await repository.GetAsync(request.Id, token);
        BusinessException.ThrowIfNull(result, "Todo was not found");
        return result.Id;
    }
}