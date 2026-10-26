using Dekacore.Core.Exceptions;
using Dekacore.Domain;
using Ds.FleetManagement.Domain.ToDo.ValueObjects;

namespace Ds.FleetManagement.Domain.ToDo.Entities;

public class TodoItem : Entity<int>
{
    private TodoItem()
    {

    }

    public TodoItem(string name)
    {
        BusinessException.ThrowIf(name.Length > 250, "TodoItemIdIsInvalid");
        Name = name;
    }

    public TodoItemName Name { get; }
}
