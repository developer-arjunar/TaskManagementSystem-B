using TaskManagementSystem.Enums;
using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Models.DTOs.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        //public string? UserImage { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNo { get; set; }

        public required string Username { get; set; }

        public required UserStatus Status { get; set; }

        public DateTime JoinedDate { get; set; }

        public Role? Role { get; set; }
    }
}
