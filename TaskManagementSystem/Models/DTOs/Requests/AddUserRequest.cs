﻿namespace TaskManagementSystem.Models.DTOs.Requests
{
    public class AddUserRequest
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required Guid RoleId { get; set; }
    }
}
