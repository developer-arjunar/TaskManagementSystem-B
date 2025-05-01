using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models.DTOs.Responses
{
    public class UserSimpleResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNo { get; set; }

        public required UserStatus Status { get; set; }
    }
}
