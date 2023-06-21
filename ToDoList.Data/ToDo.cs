using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data
{
    public class ToDo
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public bool Done { get; set; }

        public Guid? ToDoListId { get; set; }

        public ToDoList? ToDoList { get; set; }

        public Guid? ParentId { get; set; }
        public ToDo Parent { get; set; }

        public List<ToDo> ToDos { get; set; }
    }
}
