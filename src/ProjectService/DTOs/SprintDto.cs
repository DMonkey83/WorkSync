namespace ProjectService.DTOs
{
    public class SprintDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string SprintName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Goal { get; set; }

        public ProjectDto Project { get; set; } = null;
    }

}