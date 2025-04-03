﻿namespace TaskManagementSystem.Models.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
