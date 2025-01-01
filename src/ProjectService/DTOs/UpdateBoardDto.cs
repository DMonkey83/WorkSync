namespace ProjectService.DTOs
{
    public class UpdateBoardDto
    {
        public Guid? ProjectId { get; set; }
        public string BoardName { get; set; }
        public string BoardType { get; set; }
    }
}