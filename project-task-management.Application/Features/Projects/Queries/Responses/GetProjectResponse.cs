using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Projects.Queries.Responses
{
    public class GetProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public string? FullName { get; set; }

        public ICollection<GetTaskResponse> Task { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
