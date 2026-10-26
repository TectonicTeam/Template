using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.ValueObjects;

public record TodoItemName(string Value) : ValueObject
{
    public static implicit operator string(TodoItemName value) => value.Value;
    public static implicit operator TodoItemName(string value) => new(value);

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}