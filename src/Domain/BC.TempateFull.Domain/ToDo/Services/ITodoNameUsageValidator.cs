namespace Ds.FleetManagement.Domain.ToDo.Services;

public interface ITodoNameUsageValidator : IValidatorService
{
    Task<bool> IsDuplicatedNameAsync(long id, string name, CancellationToken token = default);
}