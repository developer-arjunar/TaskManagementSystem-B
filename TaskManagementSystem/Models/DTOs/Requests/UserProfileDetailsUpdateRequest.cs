namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class UserProfileDetailsUpdateRequest
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNo { get; set; }
    }
}
