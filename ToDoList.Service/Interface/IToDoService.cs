using ToDoList.Data;

namespace ToDoList.Service.Interface
{
    public interface IToDoService
    {
        Task DeleteByToDoListByIdAsync(Guid id);
        Task<List<ToDo>> GetRecursiveByToDoListIdAsync(Guid id);
        Task SaveToDosAsync(Guid toDoListId, List<ToDo> toDos);
    }
}