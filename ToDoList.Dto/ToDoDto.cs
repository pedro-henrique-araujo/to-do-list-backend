using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dto
{
    public class ToDoDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public bool Done { get; set; }

        public Guid? ToDoListId { get; set; }

        public List<ToDoDto>? ToDos { get; set; }
    }
}
