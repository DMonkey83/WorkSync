using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("IssueSequences")]
    public class IssueSequence
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public int LastNumber { get; set; }
    }
}