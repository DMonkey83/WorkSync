namespace SearchService.RequestHelpers
{
    public class SearchParams
    {
        // Basic Search
        public string SearchTerm { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Filtering
        public string FilterBy { get; set; } // Single filter field
        public string OrderBy { get; set; } // Sorting field

        // Specific Filters
        public int? OriginalEstimate { get; set; }
        public int? RemainingEstimate { get; set; }
        public int? TimeSpent { get; set; }
        public string IssuePriorityName { get; set; }
        public string IssueStatusName { get; set; }
        public string IssueTypeName { get; set; }
        public DateTime? DueDateFrom { get; set; } // Start of due date range
        public DateTime? DueDateTo { get; set; } // End of due date range

        // Multi-value Filters
        public List<string> IssueLabels { get; set; } = new List<string>(); // Filter by multiple labels

        // Advanced Combined Filters
        public List<FilterCriteria> CombinedFilters { get; set; } = new List<FilterCriteria>();
    }

    // Supporting class for combined filters
    public class FilterCriteria
    {
        public string FilterBy { get; set; } // Field name to filter
        public string Value { get; set; } // Single value filter
        public List<string> Values { get; set; } = new List<string>(); // Multi-value filter
        public DateTime? From { get; set; } // Start range for date/numeric filters
        public DateTime? To { get; set; } // End range for date/numeric filters
    }
}