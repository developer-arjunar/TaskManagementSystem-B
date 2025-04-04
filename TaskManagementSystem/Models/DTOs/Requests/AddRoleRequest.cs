namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class AddRoleRequest
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
