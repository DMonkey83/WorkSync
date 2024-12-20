using Microsoft.EntityFrameworkCore;
using ProjectService.Entities;

namespace ProjectService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<ProjectDbContext>());
        }

        private static void SeedData(ProjectDbContext context)
        {
            context.Database.Migrate();

            if (context.Projects.Any())
            {
                Console.WriteLine("Database already seeded");
                return;
            }

            var projects = new List<Project>()
            {
                // Add projects here
                new Project {
                    Id = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    ProjectName = "Project 1",
                    ProjectKey = "PROJ1",
                    Description = "Project 1 Description",
                    LeadUserId = Guid.Parse("1234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    CreatedAt = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    Status = ProjectStatus.InProgress
                },

                new Project {
                    Id = Guid.Parse("82349c1b-0f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    ProjectName = "Project 2",
                    ProjectKey = "PROJ2",
                    Description = "Project 2 Description",
                    LeadUserId = Guid.Parse("2234ac1b-334b-4b1e-9e3b-3f4b4b1e9e3b"),
                    CreatedAt = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    Status = ProjectStatus.InProgress
                }
            };
            var components = new List<Component>()
            {
                // Add components here
                new Component {
                    Id = Guid.Parse("9eabb140-f2aa-4fa7-8caf-9aa6fac73a05"),
                    ComponentName = "Component 1",
                    Description = "Component 1 Description",
                    ProjectId = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    CreatedAt = DateTime.UtcNow,
                },
            };

            var issuePriorities = new List<IssuePriority>()
            {
                new IssuePriority {
                    Id = Guid.Parse("9234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    PriorityName = "High",
                }
            };

            var issueTypes = new List<IssueType>()
            {
                new IssueType {
                    Id = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    IssueTypeName = "Task",
                }
            };

            var sprints = new List<Sprint>()
            {
                new Sprint {
                    Id = Guid.Parse("9234ac1b-334b-4b1e-9e3b-3f4b4b1e9e3b"),
                    SprintName = "Sprint 1",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    ProjectId = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    Goal = "Sprint 1 Goal",
                }
            };

            var issues = new List<Issue>()
            {
                new Issue {
                    Id = Guid.Parse("9234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e34"),
                    ComponentId = Guid.Parse("9eabb140-f2aa-4fa7-8caf-9aa6fac73a05"),
                    Description = "Issue 1 Description",
                    CreatedAt = DateTime.UtcNow,
                    ProjectId = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    DueDate = DateTime.UtcNow.AddDays(30),
                    IssueTypeId = Guid.Parse("8234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    PriorityId = Guid.Parse("9234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e3b"),
                    StatusId = Guid.Parse("9234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e31"),
                    IssueStatus = new IssueStatus {
                        Id = Guid.Parse("9234ac1b-3f4b-4b1e-9e3b-3f4b4b1e9e31"),
                        StatusName = "Open",
                    },
                    Summary = "Issue 1 Summary",
                    RemainingEstimate = 10,
                    OriginalEstimate = 20,
                    TimeSpent = 10,
                    UpdatedAt = DateTime.UtcNow.AddDays(10),
                    SprintId = Guid.Parse("9234ac1b-334b-4b1e-9e3b-3f4b4b1e9e3b"),
                }
            };

            context.AddRange(projects);
            context.AddRange(sprints);
            context.AddRange(issuePriorities);
            context.AddRange(issueTypes);
            context.AddRange(issues);
            context.AddRange(components);
            context.SaveChanges();
        }
    }
}