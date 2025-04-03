namespace TaskManagementSystem.Models.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public required string TaskComment { get; set; }

        public DateTime CreatedDate { get; set; }

        public required Guid TaskId { get; set; }

        public Task? Task { get; set; }
    }
}
