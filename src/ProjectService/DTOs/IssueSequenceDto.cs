namespace ProjectService.DTOs
{
    public class IssueSequenceDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public int LastNumber { get; set; }
    }
}