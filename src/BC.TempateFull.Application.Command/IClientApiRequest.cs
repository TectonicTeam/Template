using ClubIdentity.Core.Abstractions;

namespace BC.TempateFull.Application.Command
{
    public interface IClientApiRequest : IClientRequest
    {
        int ClientId { get; set; }
    }
}
