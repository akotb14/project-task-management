using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Domain.Entities
{
    public class Project
    {
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
} 
