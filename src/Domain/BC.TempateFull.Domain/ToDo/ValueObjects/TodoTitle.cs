using Dekacore.Core.Exceptions;
using Dekacore.Domain;

namespace Ds.FleetManagement.Domain.ToDo.ValueObjects;

public record TodoTitle: ValueObject
{

    public string Value { get; }

   

    public TodoTitle(string value)
    {
        BusinessException.ThrowIfNullOrEmpty(value, "TodoTitleIsNull");

        if (value.Length > 256)
            BusinessException.Throw("TotoTitleIsTooLong");

        if (value.Length < 5 )
            BusinessException.Throw("TotoTitleIsTooShort");



        Value = value;

    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(TodoTitle title) => title.Value;
    public static implicit operator TodoTitle(string title) => new(title);

}