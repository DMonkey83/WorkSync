using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class IssueController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Issue>> SearchIssues([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Issue, Issue>();

            query = searchParams.OrderBy switch
            {
                "issueKey" => query.Sort(x => x.Ascending(a => a.IssueKey)),
                "updatedAt" => query.Sort(x => x.Ascending(a => a.UpdatedAt)),
                "issuePriorityName" => query.Sort(x => x.Ascending(a => a.IssuePriorityName)),
                "issueStatusName" => query.Sort(x => x.Ascending(a => a.IssueStatusName)),
                "issueTypeName" => query.Sort(x => x.Ascending(a => a.IssueTypeName)),
                "dueDate" => query.Sort(x => x.Ascending(a => a.DueDate)),
                "originalEstimate" => query.Sort(x => x.Ascending(a => a.OriginalEstimate)),
                "remainingEstimate" => query.Sort(x => x.Ascending(a => a.RemainingEstimate)),
                "timeSpent" => query.Sort(x => x.Ascending(a => a.TimeSpent)),
                _ => query.Sort(x => x.Ascending(a => a.IssueKey))
            };


            // Apply filters
            if (searchParams.FilterBy != null)
            {
                query = searchParams.FilterBy switch
                {
                    "overspentTime" =>
                // Filter where TimeSpent is greater than OriginalEstimate
                    query.Match(x => x.Where(i => i.TimeSpent > i.OriginalEstimate)),
                    "dueDate" when searchParams.DueDateFrom.HasValue && searchParams.DueDateTo.HasValue =>
                        query.Match(x =>
                            x.Where(i => i.DueDate >= searchParams.DueDateFrom && i.DueDate <= searchParams.DueDateTo)),
                    "dueDateFrom" when searchParams.DueDateFrom.HasValue =>
                        query.Match(x =>
                            x.Where(i => i.DueDate >= searchParams.DueDateFrom)),
                    "dueDateTo" when searchParams.DueDateFrom.HasValue && searchParams.DueDateTo.HasValue =>
                        query.Match(x =>
                            x.Where(i => i.DueDate <= searchParams.DueDateTo)),
                    "issueLabels" when searchParams.IssueLabels != null && searchParams.IssueLabels.Any() =>
                        query.Match(x =>
                            x.Where(i => i.IssueLabels.Any(l => searchParams.IssueLabels.Contains(l.LabelName.ToString())))),
                    _ => query
                };
            }

            // Apply combined filters
            if (searchParams.CombinedFilters?.Any() == true)
            {
                foreach (var criterion in searchParams.CombinedFilters)
                {
                    query = criterion.FilterBy switch
                    {
                        "status" => query.Match(x => x.Where(i => i.IssueStatusName == criterion.Value)),
                        "priority" => query.Match(x => x.Where(i => i.IssuePriorityName == criterion.Value)),
                        "labels" when criterion.Values != null =>
                            query.Match(x =>
                                x.Where(i => i.IssueLabels.Any(l => criterion.Values.Contains(l.LabelName.ToString())))),
                        _ => query
                    };
                }
            }

            if (searchParams.RemainingEstimate.HasValue)
            {
                Console.WriteLine("Remaining Estimate: " + searchParams.RemainingEstimate.Value);
                query.Match(x => x.Where(i => i.RemainingEstimate == searchParams.RemainingEstimate.Value));
            }
            if (searchParams.OriginalEstimate.HasValue)
            {
                Console.WriteLine("Original Estimate: " + searchParams.OriginalEstimate);
                query.Match(x => x.Where(i => i.OriginalEstimate == searchParams.OriginalEstimate.Value));
            }
            if (searchParams.TimeSpent.HasValue)
            {
                Console.WriteLine("Time Spent Again: " + searchParams.TimeSpent.HasValue);
                query.Match(x => x.Where(i => i.TimeSpent == searchParams.TimeSpent));
            }
            if (!string.IsNullOrEmpty(searchParams.IssuePriorityName))
            {
                Console.WriteLine("Issue Priority Name: " + searchParams.IssuePriorityName);
                query.Match(x => x.Where(i => i.IssuePriorityName == searchParams.IssuePriorityName));
            }
            if (!string.IsNullOrEmpty(searchParams.IssueStatusName))
            {
                Console.WriteLine("Issue Status Name: " + searchParams.IssueStatusName);
                query.Match(x => x.Where(i => i.IssueStatusName == searchParams.IssueStatusName));
            }
            if (!string.IsNullOrEmpty(searchParams.IssueTypeName))
            {
                Console.WriteLine("Issue Type Name: " + searchParams.IssueTypeName);
                query.Match(x => x.Where(i => i.IssueTypeName == searchParams.IssueTypeName));
            }
            if (!string.IsNullOrEmpty(searchParams.DueDateTo.ToString()))
            {
                Console.WriteLine("Due Date: " + searchParams.DueDateTo);
                query.Match(x => x.Where(i => i.DueDate <= searchParams.DueDateTo));
            }
            if (!string.IsNullOrEmpty(searchParams.DueDateFrom.ToString()))
            {
                Console.WriteLine("Due Date: " + searchParams.DueDateFrom);
                query.Match(x => x.Where(i => i.DueDate >= searchParams.DueDateFrom));
            }
            if (searchParams.IssueLabels.ToList().Count > 0)
            {
                Console.WriteLine("Issue Labels: " + searchParams.IssueLabels);
                query.Match(x => x.Where(i => i.IssueLabels.Any(l => searchParams.IssueLabels.Contains(l.LabelName.ToString()))));
            }


            query
            .PageNumber(searchParams.PageNumber)
            .PageSize(searchParams.PageSize);

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}