namespace BC.TempateFull.Application.Query.Todos;

public record GetTodoByIdResult(long Id, string Title, string Value, string? Tag , string[] Items);