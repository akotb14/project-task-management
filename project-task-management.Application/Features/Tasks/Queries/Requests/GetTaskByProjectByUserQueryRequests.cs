using MediatR;
using project_task_management.Application.Features.Tasks.Queries.Responses;
using project_task_management.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Tasks.Queries.Requests
{
    public class GetTaskByProjectByUserQueryRequests : IRequest<Response<List<GetTasksByProjectByUserQueryResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
