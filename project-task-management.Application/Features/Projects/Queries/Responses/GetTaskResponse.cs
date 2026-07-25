using project_task_management.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Projects.Queries.Responses
{
    public class GetTaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatusEnum Status { get; set; }
        public TaskPriorityEnum Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
