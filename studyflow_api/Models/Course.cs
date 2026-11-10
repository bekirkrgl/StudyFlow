
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class Course
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Category? Category { get; set; }
    }
}
