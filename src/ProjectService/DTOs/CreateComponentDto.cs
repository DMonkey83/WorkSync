namespace ProjectService.DTOs
{
    public class CreateComponentDto
    {
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? LeadUserId { get; set; }
    }
}