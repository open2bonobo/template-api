using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}