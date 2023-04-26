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
        [Range(1,5)]
        public int Priority { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}