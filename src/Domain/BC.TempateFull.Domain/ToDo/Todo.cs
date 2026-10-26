
using Dekacore.Core.Exceptions;
using Dekacore.Domain;
using Dekacore.Persistence;
using Ds.FleetManagement.Domain.ToDo.Entities;
using Ds.FleetManagement.Domain.ToDo.Enums;
using Ds.FleetManagement.Domain.ToDo.Events;
using Ds.FleetManagement.Domain.ToDo.Services;
using Ds.FleetManagement.Domain.ToDo.ValueObjects;

namespace Ds.FleetManagement.Domain.ToDo;

public class Todo : Aggregate, IAuditableEntity, IRowVersionControl, IConcurrencyControl
{
    private Todo()
    {
    }
    private Todo(long id, string title, string value, string tag, TodoType type) : base(id)
    {
        OptionType = type;
        Title = title;
        Value = value; 
        Tag = tag;
        
    }

    public TodoTitle Title { get; private set; } = null!;
    public TodoValue Value { get; private set; } = null!;
    public TodoTag Tag { get; private set; } = null!;
    public TodoType? OptionType { get; private set; } = null!;

    private readonly List<TodoItem> _items = [];
    public IReadOnlyCollection<TodoItem> Items => _items;


    public float Total { get; private set; }

    public static async Task<Todo>  CreateTodoAsync(long id, string title, string value, string tag, TodoType type,ITodoNameUsageValidator nameValidator)
    {
        if (await nameValidator.IsDuplicatedNameAsync(id, title))
        {
            BusinessException.Throw("TodoNameAlreadyExists");
        }

        var todo =  new Todo(id,title,value,tag,type);
        todo.AddEvent(new TodoCreated(todo));

        return todo;
    }



    public async Task ChangeTitleAsync(string title, ITodoNameUsageValidator nameValidator)
    {

        if(await nameValidator.IsDuplicatedNameAsync(Id, title))
        {
            BusinessException.Throw("TodoNameAlreadyExists");
        }

        Title = title.Trim();
        AddEvent(new TodoTitleChanged(Id, Title));
    }

    public void ChangeValue(string value)
    {
        BusinessException.ThrowIfLengthLessThan(value.Trim(), 3, "Todo's value required minimum 3 characters");
        BusinessException.ThrowIfLengthGreaterThan(value.Trim(), 20, "Todo's value required maximum 10 characters");
        Value = value.Trim();
        AddEvent(new TodoValueChanged(Id, Value));
    }

    public void ChangeTag(string tag)
    {
        BusinessException.ThrowIfNullOrEmpty(tag?.Trim(), "Invalid Todo tag");
        BusinessException.ThrowIfLengthLessThan(tag?.Trim(), 3, "Todo's tag required minimum 3 characters");
        BusinessException.ThrowIfLengthGreaterThan(tag?.Trim(), 10, "Todo's tag required maximum 10 characters");
        Tag = tag.Trim();
        AddEvent(new TodoTagChanged(Id, Tag));
    }

    public void AddItem(string name)
    {
        _items.Add(new TodoItem(name));
       
        AddEvent(new TodoItemAdded(Id,name));
    }

    public void CountUp(int lastAmount)
    {
        Total = lastAmount + 1;
    }

}