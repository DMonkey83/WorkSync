using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class ComponentDto
    {
        
        public Guid Id { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; } = String.Empty;
        public Guid LeadUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}