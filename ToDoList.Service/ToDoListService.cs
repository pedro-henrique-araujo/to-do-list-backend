using Mapster;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Context;
using ToDoList.Data.Enum;
using ToDoList.Dto;
using ToDoList.Service.Interface;

namespace ToDoList.Service
{
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoListDbContext _dbContext;
        private readonly IToDoService _toDoService;
        private readonly IToDoListAccessService _toDoListAccessService;

        public ToDoListService(ToDoListDbContext dbContext, IToDoService toDoService, IToDoListAccessService toDoListAccessService)
        {
            _dbContext = dbContext;
            _toDoService = toDoService;
            _toDoListAccessService = toDoListAccessService;
        }

        public async Task<Pagination<ToDoListDto>> GetPaginationAsync(Guid userId, int skip = 0, int take = 5)
        {
            var pagination = new Pagination<ToDoListDto>();

            var filteredQueryable = _dbContext.Set<Data.ToDoList>().Where(t => t.ToDoListAccesses.Any(a => a.UserId == userId));

            pagination.Total = await filteredQueryable.CountAsync();

            var items = await filteredQueryable.Skip(skip).Take(take).ToListAsync();

            pagination.Items = items.Adapt<List<ToDoListDto>>();

            return pagination;
        }

        public async Task<ToDoListDto> GetByIdAsync(Guid userId, Guid id)
        {
            var entity = await _dbContext.Set<Data.ToDoList>()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (entity == null) return null;

            var accessType = await _toDoListAccessService.AddAccessIfNotExistsAndReturnAccessTypeAsync(new()
            {
                ToDoListId = entity.Id,
                UserId = userId,
                AccessType = AccessType.Editor
            });

            await _dbContext.SaveChangesAsync();

            entity.ToDos = await _toDoService.GetRecursiveByToDoListIdAsync(entity.Id);
            var output =  entity.Adapt<ToDoListDto>();
            output.AccessType = accessType;
            return output;
        }

        public async Task<ToDoListDto> InsertAsync(Guid userId, ToDoListDto toDoList)
        {
            var entity = toDoList.Adapt<Data.ToDoList>();
            entity.Id = Guid.NewGuid();

            if (entity.ToDos is not null)
            {
                await _toDoService.SaveToDosAsync(entity.Id, entity.ToDos.Adapt<List<Data.ToDo>>());
                entity.ToDos.Clear();
            }


            var insertEntry = _dbContext.Entry(entity);

            insertEntry.State = EntityState.Added;

            _toDoListAccessService.AddAccess(new()
            {
                ToDoListId = entity.Id,
                UserId = userId,
                AccessType = AccessType.Owner
            });

            await _dbContext.SaveChangesAsync();

            return entity.Adapt<ToDoListDto>();
        }


        public async Task<ToDoListDto> UpdateAsync(Guid userId, ToDoListDto toDoList)
        {
            var entity = toDoList.Adapt<Data.ToDoList>();

            if (entity.ToDos is not null)
            {
                await _toDoService.SaveToDosAsync(entity.Id, entity.ToDos.Adapt<List<Data.ToDo>>());
                entity.ToDos.Clear();
            }

            var updateEntry = _dbContext.Entry(entity);

            updateEntry.State = EntityState.Modified;

            await _toDoListAccessService.AddAccessIfNotExistsAndReturnAccessTypeAsync(new()
            {
                ToDoListId = entity.Id,
                UserId = userId,
                AccessType = AccessType.Editor
            });

            await _dbContext.SaveChangesAsync();

            return entity.Adapt<ToDoListDto>();
        }


        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var entity = await _dbContext.FindAsync<Data.ToDoList>(id);

            if (entity is null) return;

            if (await _toDoListAccessService.AccessExistsAsync(new()
            {
                ToDoListId = entity.Id,
                UserId = userId,
                AccessType = AccessType.Owner
            }) == false) throw new Exception("Access denied");

            _toDoListAccessService.DeleteByToDoListByIdAsync(entity.Id);
            await _toDoService.DeleteByToDoListByIdAsync(entity.Id);

            _dbContext.Entry(entity).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();

        }

    }
}