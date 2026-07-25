using project_task_management.Domain.Entities;
using project_task_management.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Tasks.Queries.Responses
{
    public class GetTasksByProjectByUserQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public TaskPriorityEnum Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; } 
    }
}
