using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Models.DTOs.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public required string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public required string Status { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public UserResponse Assignee { get; set; }
    }
}
