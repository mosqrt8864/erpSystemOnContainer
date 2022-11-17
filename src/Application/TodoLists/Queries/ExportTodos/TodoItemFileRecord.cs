using ERPSystem.Application.Common.Mappings;
using ERPSystem.Domain.Entities;

namespace ERPSystem.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
