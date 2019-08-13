namespace TaskTrackingSystem.DAL.DbContext
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.DAL.Models;

    public class TaskTrackingSystemContext : IdentityDbContext<ApplicationUser>
    {
        static TaskTrackingSystemContext()
        {
            Database.SetInitializer(new TaskTrackingSystemDbInitializer());
        }

        public TaskTrackingSystemContext()
            : base("TaskTrackingSystemContext")
        {
        }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<WorkTask> WorkTasks { get; set; }

        public static TaskTrackingSystemContext Create()
        {
            return new TaskTrackingSystemContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserPositions)
                .WithRequired(e => e.PositionUser)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectPositions)
                .WithRequired(e => e.PositionProject)
                .HasForeignKey(e => e.IdProject)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.TaskProject)
                .HasForeignKey(e => e.IdProject)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.PositionTasks)
                .WithRequired(e => e.TaskPerformer)
                .HasForeignKey(e => e.IdPerformer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkTask>()
                .Property(e => e.Description)
                .IsUnicode(false);

            // Revise and remove later.
            modelBuilder.Entity<WorkTask>().Property(m => m.StartDate).IsOptional();
            modelBuilder.Entity<WorkTask>().Property(m => m.EndDate).IsOptional();
            modelBuilder.Entity<Project>().Property(m => m.StartDate).IsOptional();

            base.OnModelCreating(modelBuilder);
        }
    }
}
