using project_task_management.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Domain.Entities
{
    public class Task
    {
        public int Id { get; private set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;

        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;

        public DateTime DueDate { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

    }
}
