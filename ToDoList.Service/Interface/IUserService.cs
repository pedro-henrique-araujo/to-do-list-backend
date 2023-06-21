using ToDoList.Data;

namespace ToDoList.Service.Interface
{
    public interface IUserService
    {
        Task<User> NewUserAsync();
    }
}