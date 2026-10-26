using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.ValueObjects;

public record TodoValue(string Value) : ValueObject
{
    public static implicit operator string(TodoValue option) => option.Value;
    public static implicit operator TodoValue(string value) => new(value);

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
