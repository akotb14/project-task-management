using MediatR;
using project_task_management.Application.Features.Projects.Queries.Responses;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Projects.Command.Requests
{
    public class CreateProjectRequest :IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
