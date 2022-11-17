using ERPSystem.Application.TodoLists.Queries.ExportTodos;

namespace ERPSystem.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
