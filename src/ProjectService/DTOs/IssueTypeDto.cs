using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssueTypeDto
    {
        public Guid Id { get; set; }
        public string IssueTypeName { get; set; }
        public string Description { get; set; }
    }
}