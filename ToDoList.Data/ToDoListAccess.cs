using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Enum;

namespace ToDoList.Data
{
    [PrimaryKey(nameof(UserId), nameof(ToDoListId))]
    public class ToDoListAccess
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Key]
        public Guid ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; }

        public AccessType AccessType { get; set; }
    }
}
