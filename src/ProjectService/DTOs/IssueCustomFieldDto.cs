using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssueCustomFieldDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}