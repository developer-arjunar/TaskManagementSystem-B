using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class AddTaskRequest
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}
