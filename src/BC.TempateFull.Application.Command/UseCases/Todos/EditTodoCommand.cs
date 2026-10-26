using Dekacore.Core.Exceptions;
using Dekacore.Extension.Mediator.Transaction;
using Ds.FleetManagement.Domain.ToDo;
using Ds.FleetManagement.Domain.ToDo.Services;
using MediatR;

namespace BC.TempateFull.Application.Command.UseCases.Todos;

public record EditTodoCommand(long Id, string Title, string Value, string Tag) : ITransactionalRequest<long>;
public class EditTodoCommandHandler(ITodoRepository repository, ITodoNameUsageValidator nameValidator)
    : IRequestHandler<EditTodoCommand, long>
{
    public async Task<long> Handle(EditTodoCommand request, CancellationToken token)
    {

        var attributeOption = await repository.GetAsync(request.Id, token);
        //if (request.Title == "string t5")
        {
            Thread.Sleep(10000);
        }
        BusinessException.ThrowIfNull(attributeOption, "AttributeOptionWasNotFound");
        await attributeOption.ChangeTitleAsync(request.Title,nameValidator);
        attributeOption.ChangeValue(request.Value);
        attributeOption.ChangeTag(request.Tag);
        await repository.UpdateAsync(attributeOption, token);
        return attributeOption.Id;
    }
}