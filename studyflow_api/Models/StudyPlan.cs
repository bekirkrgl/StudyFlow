
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class StudyPlan
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? CourseId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public DateOnly PlannedDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public User User { get; set; } = null!;

        public Course? Course { get; set; }
    }
}
