namespace TaskManagementSystem.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
