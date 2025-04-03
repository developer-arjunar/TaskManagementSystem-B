namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class UserAuthenticateRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
