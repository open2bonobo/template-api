using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public class TaskCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        public int Priority { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}