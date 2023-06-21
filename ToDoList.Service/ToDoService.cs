using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Context;
using ToDoList.Service.Interface;

namespace ToDoList.Service
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoListDbContext _dbContext;
        public ToDoService(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ToDo>> GetRecursiveByToDoListIdAsync(Guid toDoListId)
        {
            var toDos = await _dbContext.Set<ToDo>().Where(t => t.ToDoListId == toDoListId).AsNoTracking().ToListAsync();
            foreach (var toDo in toDos)
            {
                toDo.ToDos = await GetRecursiveByParentIdAsync(toDo.Id);
            }
            return toDos;
        }

        public async Task SaveToDosAsync(Guid toDoListId, List<Data.ToDo> toDos)
        {
            var flatToDosInDb = FlatMap(await GetRecursiveByToDoListIdAsync(toDoListId));
            var flatToDosToInsertOrUpdate = FlatMap(toDos);
            var toDosToRemove = flatToDosInDb.Where(t => flatToDosToInsertOrUpdate.Any(tdb => tdb.Id == t.Id) == false);

            foreach (var toDo in flatToDosToInsertOrUpdate)
            {
                if (toDo.ParentId is null)
                {
                    toDo.ToDoListId = toDoListId;
                }
                await SaveToDoAsync(toDo);
            }

            _dbContext.Set<ToDo>().RemoveRange(toDosToRemove);
        }

        private async Task SaveToDoAsync(ToDo toDo)
        {
            var toDoInDb = await _dbContext.Set<ToDo>().FirstOrDefaultAsync(t => t.Id == toDo.Id);
            if (toDoInDb is null)
            {
                toDo.Id = toDo.Id == Guid.Empty ? Guid.NewGuid() : toDo.Id;
                _dbContext.Entry(toDo).State = EntityState.Added;
                return;
            }

            _dbContext.Entry(toDoInDb).State = EntityState.Detached;
            _dbContext.Entry(toDo).State = EntityState.Modified;
        }

        public async Task DeleteByToDoListByIdAsync(Guid toDoListId)
        {
            var flatToDos = FlatMap(await GetRecursiveByToDoListIdAsync(toDoListId));

            _dbContext.Set<ToDo>().RemoveRange(flatToDos);
        }

        private List<ToDo> FlatMap(List<ToDo> toDos, Guid? parentId = null)
        {
            var flatList = new List<ToDo>();
            foreach (var toDo in toDos)
            {
                if (parentId.HasValue)
                {
                    toDo.ToDoListId = null;
                    toDo.ParentId = parentId;

                }

                if (toDo.Id == Guid.Empty)
                {
                    toDo.Id = Guid.NewGuid();
                }

                if (toDo.ToDos is not null)
                {
                    flatList.AddRange(FlatMap(toDo.ToDos, toDo.Id));
                    toDo.ToDos.Clear();
                }

                flatList.Add(toDo);
            }
            return flatList;
        }

        private async Task<List<ToDo>> GetRecursiveByParentIdAsync(Guid parentId)
        {
            var toDos = await _dbContext.Set<ToDo>().Where(t => t.ParentId == parentId).ToListAsync();

            foreach (var toDo in toDos)
            {
                toDo.ToDos = await GetRecursiveByParentIdAsync(toDo.Id);
            }

            return toDos;
        }
    }
}
