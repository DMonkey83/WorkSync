using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class UpdateIssueHistoryDto
    {
        
        public Guid ChangedBy { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}