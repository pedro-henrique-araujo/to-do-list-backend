using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Enum;

namespace ToDoList.Dto
{
    public class ToDoListDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<ToDoDto>? ToDos { get; set; }
        public AccessType AccessType { get; set; }
    }
}