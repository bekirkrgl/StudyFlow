
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class StudySession
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? CourseId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public User User { get; set; } = null!;

        public Course? Course { get; set; }
    }
}
