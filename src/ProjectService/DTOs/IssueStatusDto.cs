namespace ProjectService.DTOs
{
    public class IssueStatusDto
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public List<IssueDto> Issues { get; set; } = new List<IssueDto>();
    }
}