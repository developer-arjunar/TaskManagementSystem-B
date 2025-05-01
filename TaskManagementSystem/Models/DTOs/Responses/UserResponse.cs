using TaskManagementSystem.Enums;
using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Models.DTOs.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNo { get; set; }

        public required string Username { get; set; }

        public required UserStatus Status { get; set; }

        public Role? Role { get; set; }
    }
}
