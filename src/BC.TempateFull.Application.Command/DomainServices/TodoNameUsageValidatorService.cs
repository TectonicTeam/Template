using Ds.FleetManagement.Domain.ToDo;
using Ds.FleetManagement.Domain.ToDo.Services;
using BC.TempateFull.Application.Command.Specifications;

namespace BC.TempateFull.Application.Command.DomainServices;

public class TodoNameUsageValidatorService(ITodoRepository repository) : ITodoNameUsageValidator
{
    public Task<bool> IsDuplicatedNameAsync(long id, string name, CancellationToken token = default)
    {
        var spec = new IsThereAnyTidoWithTitleOfSpec(id, name);
        return repository.ExistsAsync(spec, token);
    }
}