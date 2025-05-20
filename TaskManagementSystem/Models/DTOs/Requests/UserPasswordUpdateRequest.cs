namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class UserPasswordUpdateRequest
    {
        public required string OldPassword { get; set; }

        public required string NewPassword { get; set; }
    }
}
