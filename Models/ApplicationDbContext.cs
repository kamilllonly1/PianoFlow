using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PianoFlow.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Здесь будут DbSet для других сущностей (уроки, упражнения и т.д.)
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseAttempt> ExerciseAttempts { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
} 