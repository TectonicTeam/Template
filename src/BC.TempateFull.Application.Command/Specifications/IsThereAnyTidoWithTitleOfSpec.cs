using Ardalis.Specification;
using Ds.FleetManagement.Domain.ToDo;


namespace BC.TempateFull.Application.Command.Specifications;

public sealed class IsThereAnyTidoWithTitleOfSpec : Specification<Todo>
{
    public IsThereAnyTidoWithTitleOfSpec(long id, string name)
    {
        Query.Where(x => x.Id != id && string.Equals(name.Trim(), x.Title.Value.Trim())).AsNoTracking();
    }
}