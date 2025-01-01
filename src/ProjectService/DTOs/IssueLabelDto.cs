using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssueLabelDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid LabelName { get; set; }
    }
}