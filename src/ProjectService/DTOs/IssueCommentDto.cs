using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssueCommentDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}