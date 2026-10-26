using DekaClub.Application.Contract.Models.MembersManagement.Members;

namespace BC.TempateFull.Application.Command.Services
{
    public interface IMembersService
    {
        Task<GetMemberResult> GetMemberAsync(long memberId, CancellationToken cancellationToken = default);
    }


}
