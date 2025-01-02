namespace ProjectService.DTOs
{
    public class UpdateComponentDto
    {
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public Guid? ProjectId { get; set; }
    }
}