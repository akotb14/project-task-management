using MediatR;
using project_task_management.Application.Features.Projects.Queries.Responses;
using project_task_management.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Projects.Command.Requests
{
    public class DeleteProjectRequest : IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
