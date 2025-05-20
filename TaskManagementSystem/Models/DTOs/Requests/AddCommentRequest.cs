namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class AddCommentRequest
    {
        public required string TaskComment { get; set; }

        public required string CreatedBy { get; set; }

        public required Guid TaskId { get; set; }
    }
}
