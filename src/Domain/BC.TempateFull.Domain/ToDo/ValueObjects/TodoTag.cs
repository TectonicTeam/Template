using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.ValueObjects;

public record TodoTag(string Value) : ValueObject
{
    public static implicit operator string(TodoTag tag) => tag.Value;
    public static implicit operator TodoTag(string tag) => new(tag);

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}