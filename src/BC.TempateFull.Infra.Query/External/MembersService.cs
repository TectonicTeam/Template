using DekaClub.Application.Contract.Models.MembersManagement.Members;
using Dekacore.InternalService.Http;
using BC.TempateFull.Application.Command.Services;
using Microsoft.Extensions.Logging;

namespace DekaWebHook.Infra.Query.External
{
    internal class MembersService(ILogger<MembersService> logger, HttpClient httpClient) : BaseInternalService(logger, httpClient), IMembersService
    {

        public Task<GetMemberResult> GetMemberAsync(long memberId, CancellationToken cancellationToken = default)
        {

            return GetAsync<GetMemberResult>($"InternalService/Member/GetMember?Id={memberId}", cancellationToken)!;
        }


    }
}
