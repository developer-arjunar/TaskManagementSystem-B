using System.Text.Json.Serialization;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        //public string? UserImage { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNo { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required UserStatus Status { get; set; }

        public DateTime JoinedDate { get; set; }

        public required Guid RoleId { get; set; }

        public Role? Role { get; set; }

        [JsonIgnore]
        public ICollection<Task>? Tasks { get; set; }
    }
}
