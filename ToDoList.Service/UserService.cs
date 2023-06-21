using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Context;
using ToDoList.Service.Interface;

namespace ToDoList.Service
{
    public class UserService : IUserService
    {
        private ToDoListDbContext _dbContext;

        public UserService(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> NewUserAsync()
        {
            var newUser = new User { Id = Guid.NewGuid() };
            _dbContext.Entry(newUser).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return newUser;
        }
    }
}
