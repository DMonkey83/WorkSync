using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.DTOs
{
    public class IssuePriorityDto
    {
        public Guid Id { get; set; }
        public string PriorityName { get; set; }
    }
}