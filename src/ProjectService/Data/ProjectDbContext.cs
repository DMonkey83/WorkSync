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

        private void GeneratedIssueKeys()
        {
            var newIssues = ChangeTracker.Entries<Issue>()
                .Where(e => e.State == EntityState.Added && string.IsNullOrEmpty(e.Entity.IssueKey))
                .Select(e => e.Entity);
            
            foreach (var issue in newIssues)
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
                 issue.IssueKey = $"{project.ProjectKey}-{issueSequence.LastNumber:D4}"; // Format number with leading zeroes
            }
        }
    }
}