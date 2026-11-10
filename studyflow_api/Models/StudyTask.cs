
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class StudyTask
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? CourseId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(30)]
        public string TaskType { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Course? Course { get; set; }
    }
}
