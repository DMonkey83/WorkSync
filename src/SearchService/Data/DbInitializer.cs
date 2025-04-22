using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task DBInit(WebApplication app)
        {
            await DB.InitAsync("SearchDB", MongoClientSettings
                .FromConnectionString("mongodb://root:mongopw@localhost:27017"));
            await DB.Index<Issue>()
                .Key(i => i.IssueKey, KeyType.Text)
                .Key(i => i.ParentIssueId, KeyType.Text)
                .Key(i => i.UpdatedAt, KeyType.Text)
                .Key(i => i.IssuePriorityName, KeyType.Text)
                .Key(i => i.IssueStatusName, KeyType.Text)
                .Key(i => i.IssueTypeName, KeyType.Text)
                .Key(i => i.DueDate, KeyType.Text)
                .Key(i => i.OriginalEstimate, KeyType.Text)
                .Key(i => i.RemainingEstimate, KeyType.Text)
                .Key(i => i.TimeSpent, KeyType.Text)
                .Key(i => i.IssueLabels, KeyType.Text)
                .CreateAsync();
            
            var count = await DB.CountAsync<Issue>();

            using var scope = app.Services.CreateScope();

            var httpClient = scope.ServiceProvider.GetRequiredService<IssueSvcHttpClient>();

            var issues = await httpClient.GetIssuesForSearchDb();

            Console.WriteLine($"Issues count: {issues.Count}");

            if (issues.Count > 0) await DB.SaveAsync(issues);
        }
        
    }
}