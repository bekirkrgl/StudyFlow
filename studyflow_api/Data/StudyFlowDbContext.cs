
using Microsoft.EntityFrameworkCore;
using StudyFlow.Api.Models;

namespace StudyFlow.Api.Data
{
    public class StudyFlowDbContext : DbContext
    {
        public StudyFlowDbContext(
            DbContextOptions<StudyFlowDbContext> options)
            : base(options)
        {
        }

        // Veritabani tablolari
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<StudyTask> Tasks => Set<StudyTask>();
        public DbSet<StudyPlan> StudyPlans => Set<StudyPlan>();
        public DbSet<StudySession> StudySessions
            => Set<StudySession>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Roles
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(r => r.Name).IsUnique();

                entity.HasMany(r => r.Users)
                    .WithOne(u => u.Role)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();

                entity.HasMany(u => u.Courses)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();

                entity.HasMany(c => c.Courses)
                    .WithOne(c => c.Category)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Topics
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasOne(t => t.Course)
                    .WithMany()
                    .HasForeignKey(t => t.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Tasks
            modelBuilder.Entity<StudyTask>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.Course)
                    .WithMany()
                    .HasForeignKey(t => t.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // StudyPlans
            modelBuilder.Entity<StudyPlan>(entity =>
            {
                entity.Property(p => p.PlannedDate)
                    .HasColumnType("date");

                entity.Property(p => p.StartTime)
                    .HasColumnType("time");

                entity.Property(p => p.EndTime)
                    .HasColumnType("time");

                entity.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Course)
                    .WithMany()
                    .HasForeignKey(p => p.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // StudySessions
            modelBuilder.Entity<StudySession>(entity =>
            {
                entity.HasOne(s => s.User)
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(s => s.Course)
                    .WithMany()
                    .HasForeignKey(s => s.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
