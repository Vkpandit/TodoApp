using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [DisplayName("Task Name")]
        public string TaskName { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        // New Property to mark task as done
        public bool IsCompleted { get; set; } = false;

        public DateTime? DoneDateTime { get; set; }
    }
}
