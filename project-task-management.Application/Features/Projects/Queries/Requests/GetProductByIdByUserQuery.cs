using MediatR;
using project_task_management.Application.Features.Projects.Queries.Responses;
using project_task_management.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Projects.Queries.Requests
{
    public class GetProductByIdByUserQuery : IRequest<Response<GetProjectResponse>>
    {
        public int Id { get; set; }
    }
}
