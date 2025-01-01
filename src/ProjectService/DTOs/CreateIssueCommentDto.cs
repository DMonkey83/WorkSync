using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class CreateIssueCommentDto
    {
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
    }
}