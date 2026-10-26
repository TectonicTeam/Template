using Ds.FleetManagement.Domain.ToDo.Enums;

namespace BC.TempateFull.Application.Query.Todos;

public record GetAllTodosResult(long Id, string Title, string Value, TodoType? type, DateTimeOffset? date, float? total, string? Tag, string[] items) { }

