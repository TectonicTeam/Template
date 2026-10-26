using Ds.FleetManagement.Domain.ToDo.Enums;

namespace Catalog.Infra.Queries.Entities;

internal class Todo
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Tag { get; set; }
    public float? Total { get; set; }
    public DateTimeOffset? CreatedDateTime { get; set; }
    public TodoType? OptionType { get; set; }

    public List<TodoItem>? Items { get; set; }
}

internal class TodoItem
{
    public int Id { get; set; }

    public long TodoId { get; private set; }
    public required string Name { get; set; }

    public required Todo Todo { get; set; }
}