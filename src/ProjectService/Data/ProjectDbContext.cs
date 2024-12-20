using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectService.Entities;

namespace ProjectService.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<IssueComment> IssueComments { get; set; } = null!;
        public DbSet<IssueCustomField> IssueCustomFields { get; set; } = null!;
        public DbSet<IssueLabel> IssueLabels { get; set; } = null!;
        public DbSet<IssueStatus> IssueStatuses { get; set; } = null!;
        public DbSet<IssuePriority> IssuePriorities { get; set; } = null!;
        public DbSet<IssueHistory> IssueHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Component>()
                .HasOne(c => c.Project) // Navigation property to Project
                .WithMany(p => p.Components) // Inverse navigation to Components
                .HasForeignKey(c => c.ProjectId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectKey)
                .IsUnique();

            modelBuilder.Entity<Issue>()
                .HasOne(s => s.IssueStatus) // navigation property to issues
                .WithMany(p => p.Issues) // inverse navigation to issues
                .HasForeignKey(c => c.StatusId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            modelBuilder.Entity<Issue>()
                .HasOne(s => s.IssuePriority) // navigation property to issues
                .WithMany(p => p.Issues) // inverse navigation to issues
                .HasForeignKey(c => c.PriorityId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            modelBuilder.Entity<Issue>()
                .HasOne(s => s.IssueType) // navigation property to issues
                .WithMany(p => p.Issues) // inverse navigation to issues
                .HasForeignKey(c => c.IssueTypeId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            modelBuilder.Entity<Issue>()
                .HasOne(s => s.IssueType) // navigation property to issues
                .WithMany(p => p.Issues) // inverse navigation to issues
                .HasForeignKey(c => c.IssueTypeId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            base.OnModelCreating(modelBuilder);
        }
    }
}