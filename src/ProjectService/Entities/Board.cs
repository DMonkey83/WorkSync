using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("Boards")]
    public class Board
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public string BoardName { get; set; }
        public BoardType BoardType { get; set; } = BoardType.Scrum;

        // Navigation properties
        public List<Issue> Issues { get; set; } = [];
        
    }
}