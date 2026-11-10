
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyFlow.Api.Data;
using StudyFlow.Api.Models;

namespace StudyFlow.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Student")]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly StudyFlowDbContext _db;

        public CoursesController(StudyFlowDbContext db)
        {
            _db = db;
        }

        private int? GetUserId()
        {
            string? value = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            return int.TryParse(value, out int id)
                ? id : null;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int? userId = GetUserId();
            if (userId == null) return Unauthorized();

            var courses = await _db.Courses
                .AsNoTracking()
                .Where(c => c.UserId == userId.Value)
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    c.CategoryId,
                    c.CreatedAt
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            int? userId = GetUserId();
            if (userId == null) return Unauthorized();

            var course = await _db.Courses
                .AsNoTracking()
                .Where(c => c.Id == id && c.UserId == userId.Value)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    c.CategoryId,
                    c.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CourseRequest request)
        {
            int? userId = GetUserId();
            if (userId == null) return Unauthorized();

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Ders adi bos olamaz.");

            if (request.CategoryId.HasValue &&
                !await _db.Categories.AnyAsync(c =>
                    c.Id == request.CategoryId.Value))
            {
                return BadRequest("Kategori bulunamadi.");
            }

            var course = new Course
            {
                UserId = userId.Value,
                Name = request.Name.Trim(),
                Description = request.Description,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.Id },
                new
                {
                    course.Id,
                    course.Name,
                    course.Description,
                    course.CategoryId,
                    course.CreatedAt
                });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id, CourseRequest request)
        {
            int? userId = GetUserId();
            if (userId == null) return Unauthorized();

            var course = await _db.Courses
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId.Value);

            if (course == null) return NotFound();

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Ders adi bos olamaz.");

            if (request.CategoryId.HasValue &&
                !await _db.Categories.AnyAsync(c =>
                    c.Id == request.CategoryId.Value))
            {
                return BadRequest("Kategori bulunamadi.");
            }

            course.Name = request.Name.Trim();
            course.Description = request.Description;
            course.CategoryId = request.CategoryId;

            await _db.SaveChangesAsync();

            return Ok(new
            {
                Message = "Ders guncellendi.",
                course.Id
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? userId = GetUserId();
            if (userId == null) return Unauthorized();

            var course = await _db.Courses
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId.Value);

            if (course == null) return NotFound();

            bool hasRelatedRecords =
                await _db.Topics.AnyAsync(t => t.CourseId == id) ||
                await _db.Tasks.AnyAsync(t => t.CourseId == id) ||
                await _db.StudyPlans.AnyAsync(p => p.CourseId == id) ||
                await _db.StudySessions.AnyAsync(s => s.CourseId == id);

            if (hasRelatedRecords)
            {
                return Conflict(
                    "Derse bagli kayitlar var. Once bunlari duzenleyin.");
            }

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }

    public class CourseRequest
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
    }
}
