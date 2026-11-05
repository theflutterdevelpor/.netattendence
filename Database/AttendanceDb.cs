using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;

namespace AttendanceSystem.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== USER =====
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Role).HasMaxLength(20).IsRequired();

            // ===== CLASS =====
            modelBuilder.Entity<Class>()
                .Property(c => c.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Class>()
                .HasIndex(c => c.Name).IsUnique();

            // ===== SECTION =====
            modelBuilder.Entity<Section>()
                .Property(s => s.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Section>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== SUBJECT =====
            modelBuilder.Entity<Subject>()
                .Property(s => s.Name).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== STUDENT =====
            modelBuilder.Entity<Student>()
                .Property(s => s.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Student>()
                .Property(s => s.FatherName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Student>()
                .Property(s => s.RollNumber).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Student>()
                .Property(s => s.Email).HasMaxLength(150);
            modelBuilder.Entity<Student>()
                .Property(s => s.Phone).HasMaxLength(20);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.RollNumber).IsUnique();

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Section)
                .WithMany()
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== ATTENDANCE =====
            modelBuilder.Entity<Attendance>()
                .Property(a => a.Status).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.StudentId, a.SubjectId, a.Date })
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany()
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Section)
                .WithMany()
                .HasForeignKey(a => a.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Subject)
                .WithMany()
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== LEAVE REQUEST =====
            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.Reason).HasMaxLength(500).IsRequired();
            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.Status).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Student)
                .WithMany()
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}