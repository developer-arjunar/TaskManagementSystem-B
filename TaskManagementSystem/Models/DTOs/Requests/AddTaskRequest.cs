using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class AddTaskRequest
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }

        public DateTime DueDate { get; set; }

        public required string Status { get; set; }

        public required Guid AssigneeId { get; set; }
    }
}