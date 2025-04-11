namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class UpdateTaskRequest
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime DueDate { get; set; }

        public required string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public required string Status { get; set; }

        public required Guid AssigneeId { get; set; }
    }
}
