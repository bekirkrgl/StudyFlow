
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public Course Course { get; set; } = null!;
    }
}
