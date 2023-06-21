using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ToDoList.Data.Context
{
    public class ToDoListDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }

        public DbSet<ToDoList> ToDoLists { get; set; }

        public DbSet<ToDoListAccess> ToDoListAccesses { get; set; }

        public DbSet<ToDo> ToDos { get; set; }

        public ToDoListDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ToDoList");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
