using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskMWeb.Models
{
    public class TaskM
    {
        [Key]
        public int Task_id { get; set; }

        public int User_id { get; set; }


        [Required(ErrorMessage = "TaskName is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string? Task_Name { get; set; }


        [Required(ErrorMessage = "TaskStatus is required")]
        [Column(TypeName = "nvarchar(100)")]
        public bool Task_Status { get; set; }

    }
}
