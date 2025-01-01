using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssueHistoryDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid ChangedBy { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}