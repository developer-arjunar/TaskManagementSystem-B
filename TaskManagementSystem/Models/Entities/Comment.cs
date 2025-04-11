using System.Text.Json.Serialization;

namespace TaskManagementSystem.Models.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public required string TaskComment { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public required Guid TaskId { get; set; }

        [JsonIgnore]
        public Task? Task { get; set; }
    }
}
