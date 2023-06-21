using ToDoList.Data;
using ToDoList.Data.Enum;

namespace ToDoList.Service.Interface
{
    public interface IToDoListAccessService
    {
        Task<bool> AccessExistsAsync(ToDoListAccess access);
        void AddAccess(ToDoListAccess access);
        Task<AccessType> AddAccessIfNotExistsAndReturnAccessTypeAsync(ToDoListAccess access);
        void DeleteByToDoListByIdAsync(Guid toDoListId);
    }
}