using ToDoList.Dto;

namespace ToDoList.Service.Interface
{
    public interface IToDoListService
    {
        Task DeleteAsync(Guid userId, Guid id);
        Task<ToDoListDto> GetByIdAsync(Guid userId, Guid id);
        Task<Pagination<ToDoListDto>> GetPaginationAsync(Guid userId, int skip = 0, int take = 5);
        Task<ToDoListDto> InsertAsync(Guid userId, ToDoListDto toDoList);
        Task<ToDoListDto> UpdateAsync(Guid userId, ToDoListDto toDoList);
    }
}