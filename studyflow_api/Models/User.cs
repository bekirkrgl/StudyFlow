
using System.ComponentModel.DataAnnotations;

namespace StudyFlow.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public Role Role { get; set; } = null!;

        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
