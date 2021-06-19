using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ClassJournalProject.Models;

namespace ClassJournalProject.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        
        public DbSet<StudentAttendance> StudentAttendance { get; set; }
        public DbSet<StudentStatus> StudentStatuses { get; set; }
        public DbSet<StudentEducationLevel> StudentEducationLevels { get; set; }
        
        public DbSet<SpecialtySubjectAssignment> SpecialtySubjectAssignments { get; set; }
        public DbSet<TeacherSubjectAssignment> TeacherSubjectAssignments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder) {

            //builder.Entity<User>().ToTable("Users");

            builder.Entity<Group>().ToTable("Groups");
            builder.Entity<Lesson>().ToTable("Lessons");
            builder.Entity<Specialty>().ToTable("Specialties");
            builder.Entity<SpecialtySubjectAssignment>().ToTable("SpecialtySubjectAssignments");
            //builder.Entity<Student>().ToTable("Students");
            builder.Entity<StudentAttendance>().ToTable("StudentAttendance");
            builder.Entity<StudentEducationLevel>().ToTable("StudentEducationLevels");
            builder.Entity<StudentStatus>().ToTable("StudentStatuses");
            builder.Entity<Subject>().ToTable("Subjects");
            //builder.Entity<Teacher>().ToTable("Teachers");
            builder.Entity<TeacherSubjectAssignment>().ToTable("TeacherSubjectAssignments");

            builder.Entity<SpecialtySubjectAssignment>()
                .HasKey(s => new {s.SpecialtyId, s.SubjectId});

            builder.Entity<TeacherSubjectAssignment>()
                .HasKey(t => new { t.TeacherId, t.SubjectId });

            builder.Entity<StudentAttendance>()
                .HasKey(s => new { s.StudentId, s.LessonId });

            builder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithOne()
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Group>()
                .HasOne(g => g.Specialty)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Group>()
                .HasMany(g => g.Lessons)
                .WithOne()
                .HasForeignKey(l => l.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Specialty>()
                .HasMany(s => s.SpecialtySubjectAssignments)
                .WithOne(ssa => ssa.Specialty)
                .HasForeignKey(ssa => ssa.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SpecialtySubjectAssignment>()
                .HasOne(ssa => ssa.Specialty)
                .WithMany(s => s.SpecialtySubjectAssignments)
                .HasForeignKey(ssa => ssa.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SpecialtySubjectAssignment>()
                .HasOne(ssa => ssa.Subject)
                .WithMany(s => s.SpecialtySubjectAssignments)
                .HasForeignKey(ssa => ssa.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeacherSubjectAssignment>()
                .HasOne(tsa => tsa.Teacher)
                .WithMany(t => t.TeacherSubjectAssignments)
                .HasForeignKey(tsa => tsa.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeacherSubjectAssignment>()
                .HasOne(tsa => tsa.Subject)
                .WithMany(t => t.TeacherSubjectAssignments)
                .HasForeignKey(tsa => tsa.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Lesson>()
                .HasOne(l => l.Group)
                .WithMany(g => g.Lessons)
                .HasForeignKey(l => l.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
