using Dekacore.Core.Exceptions;

namespace BC.TempateFull.Shared.Domain.Exceptions;

public class RequestersException : BusinessException
{
    public const string WebHookAlreadyExists = nameof(WebHookAlreadyExists);
    public const string WebHookNotExists = nameof(WebHookNotExists);
    public const string GetTokenWebHookNotExists = nameof(GetTokenWebHookNotExists);

    public const string InvalidRequestBodyInGetHttpMethod = nameof(InvalidRequestBodyInGetHttpMethod);
    public RequestersException(string message) : base(message)
    {

    }
}