using MassTransit;
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
        public DbSet<Sprint> Sprints { get; set; } = null!;
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Issue> Issues { get; set; } = null!;
        public DbSet<Component> Components { get; set; } = null!;
        public DbSet<IssueType> IssueTypes { get; set; } = null!;
        public DbSet<IssueComment> IssueComments { get; set; } = null!;
        public DbSet<IssueCustomField> IssueCustomFields { get; set; } = null!;
        public DbSet<IssueLabel> IssueLabels { get; set; } = null!;
        public DbSet<IssueStatus> IssueStatuses { get; set; } = null!;
        public DbSet<IssuePriority> IssuePriorities { get; set; } = null!;
        public DbSet<IssueHistory> IssueHistories { get; set; } = null!;
        public DbSet<IssueSequence> IssueSequences { get; set; } = null!;

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

            modelBuilder.Entity<IssueLabel>()
                .HasOne(i => i.Issue) // navigation property to issues
                .WithMany(p => p.IssueLabels) // inverse navigation to issues
                .HasForeignKey(c => c.IssueId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            modelBuilder.Entity<IssueComment>()
                .HasOne(i => i.Issue) // navigation property to issues
                .WithMany(p => p.IssueComments) // inverse navigation to issues
                .HasForeignKey(c => c.IssueId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

            modelBuilder.Entity<IssueCustomField>()
                .HasOne(i => i.Issue) // navigation property to issues
                .WithMany(p => p.IssueCustomFields) // inverse navigation to issues
                .HasForeignKey(c => c.IssueId) // Foreign key in Components
                .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete



            modelBuilder.Entity<Issue>()
                .HasOne(i => i.ParentIssue)
                .WithMany()
                .HasForeignKey(i => i.ParentIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.IssueType)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.IssueTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.IssuePriority)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.IssueStatus)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Component)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Sprint)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.SprintId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Issue>()
                .HasMany(i => i.IssueLabels)
                .WithOne(p => p.Issue)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Issue>()
                .HasMany(i => i.IssueComments)
                .WithOne(p => p.Issue)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Issue>()
                .HasMany(i => i.IssueCustomFields)
                .WithOne(p => p.Issue)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }


        public override int SaveChanges()
        {
            GeneratedIssueKeys();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GeneratedIssueKeys();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private readonly object _lock = new object();

        private void GeneratedIssueKeys()
        {
            lock (_lock)
            {
                // Handle newly added issues
                var newIssues = ChangeTracker.Entries<Issue>()
                    .Where(e => e.State == EntityState.Added && string.IsNullOrEmpty(e.Entity.IssueKey))
                    .Select(e => e.Entity)
                    .ToList();

                foreach (var issue in newIssues)
                {
                    GenerateKeyForIssue(issue);
                }

                // Handle updated issues where ProjectId has changed
                var updatedIssues = ChangeTracker.Entries<Issue>()
                    .Where(e => e.State == EntityState.Modified &&
                                e.Property(p => p.ProjectId).IsModified)
                    .ToList();

                foreach (var entry in updatedIssues)
                {
                    var originalProjectId = (Guid?)entry.OriginalValues[nameof(Issue.ProjectId)];
                    var currentProjectId = entry.Entity.ProjectId;

                    // Only regenerate the IssueKey if the ProjectId has changed
                    if (originalProjectId != currentProjectId)
                    {
                        GenerateKeyForIssue(entry.Entity);
                    }
                }
            }
        }

        private void GenerateKeyForIssue(Issue issue)
        {
            var project = Projects.Find(issue.ProjectId);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found");
            }

            var issueSequence = IssueSequences.FirstOrDefault(seq => seq.ProjectId == project.Id);
            if (issueSequence == null)
            {
                issueSequence = new IssueSequence
                {
                    ProjectId = issue.ProjectId,
                    LastNumber = 0
                };
                IssueSequences.Add(issueSequence);
            }

            issueSequence.LastNumber++;
            issue.IssueKey = $"{project.ProjectKey}-{issueSequence.LastNumber:D4}";
            Console.WriteLine($"Generated Issue Key: {issue.IssueKey}");
        }



    }
}