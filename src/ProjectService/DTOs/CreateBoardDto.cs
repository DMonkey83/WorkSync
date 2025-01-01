namespace ProjectService.DTOs
{
    public class CreateBoardDto
    {
        
        public Guid ProjectId { get; set; }
        public string BoardName { get; set; }
        public string BoardType { get; set; }
    }
}