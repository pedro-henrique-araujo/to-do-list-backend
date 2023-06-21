using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Context;
using ToDoList.Data.Enum;
using ToDoList.Service.Interface;

namespace ToDoList.Service
{
    public class ToDoListAccessService : IToDoListAccessService
    {
        private ToDoListDbContext _dbContext;

        public ToDoListAccessService(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AccessExistsAsync(ToDoListAccess access)
        {
            var userId = await _dbContext.Set<ToDoListAccess>()
                .Where(a => a.UserId == access.UserId && a.ToDoListId == access.ToDoListId && a.AccessType == access.AccessType)
                .Select(a => a.UserId)
                .FirstOrDefaultAsync();
            return userId != Guid.Empty;
        }

        public void AddAccess(ToDoListAccess access)
        {
            _dbContext.Entry(access).State = EntityState.Added;
        }

        public async Task<AccessType> AddAccessIfNotExistsAndReturnAccessTypeAsync(ToDoListAccess access)
        {
            var accessInDb = await _dbContext.Set<ToDoListAccess>()
                .Where(a => a.UserId == access.UserId && a.ToDoListId == access.ToDoListId)
                .FirstOrDefaultAsync();
            if (accessInDb is not null) return accessInDb.AccessType;
            AddAccess(access);
            return access.AccessType;
        }

        public void DeleteByToDoListByIdAsync(Guid toDoListId)
        {
            var accesses = _dbContext.Set<ToDoListAccess>().Where(t => t.ToDoListId == toDoListId);

            _dbContext.Set<ToDoListAccess>().RemoveRange(accesses);

            _dbContext.SaveChanges();
        }
    }
}
