using Dekacore.Domain;

namespace BC.TempateFull.Shared.Domain.ValueObjects;

public abstract record BaseWebHookRequestBody : ValueObject
{
    private const string InvalidRequestBody = nameof(InvalidRequestBody);


    public string Value { get; } = null!;

    private BaseWebHookRequestBody() { }

    public BaseWebHookRequestBody(string body)
    {


        Value = body;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

}