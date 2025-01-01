namespace ProjectService.DTOs
{
    public class BoardDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string BoardName { get; set; }
        public string BoardType { get; set; }
    }
}