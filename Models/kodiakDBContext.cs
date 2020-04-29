using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace kodiak.Models
{
    public partial class kodiakDBContext : DbContext
    {
        public kodiakDBContext()
        {
        }

        public kodiakDBContext(DbContextOptions<kodiakDBContext> options)
            : base(options)
        {
        }
        public string lastClickedTable;

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<ActivityType> ActivityType { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseContent> CourseContent { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<LessonContent> LessonContent { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//                  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=localhost; database=kodiakDB; user id=sa;Password=B3lated#iSh");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.ActivityTypeId)
                    .IsRequired()
                    .HasColumnName("activityTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Activity_ActivityType");
            });

            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.Property(e => e.ActivityTypeId)
                    .HasColumnName("activityTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseContent>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.LessonId })
                    .HasName("CourseContent_pk");

                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.LessonId).HasColumnName("lessonID");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseContent)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CourseContent_Course");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.CourseContent)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CourseContent_Lesson");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.LessonId).HasColumnName("lessonID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LessonContent>(entity =>
            {
                entity.HasKey(e => new { e.LessonId, e.ActivityId })
                    .HasName("LessonContent_pk");

                entity.Property(e => e.LessonId).HasColumnName("lessonID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.LessonContent)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LessonContent_Activity");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonContent)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LessonContent_Lesson");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
