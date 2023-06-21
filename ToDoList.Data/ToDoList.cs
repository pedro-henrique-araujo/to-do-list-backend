using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data
{
    public class ToDoList
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<ToDo> ToDos { get; set; }

        public List<ToDoListAccess>? ToDoListAccesses { get; set; }
    }
}
