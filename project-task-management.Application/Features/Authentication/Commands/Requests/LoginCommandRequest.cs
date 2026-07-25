using MediatR;
using project_task_management.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Authentication.Commands.Requests
{
    public class LoginCommandRequest : IRequest<Response<AuthJwtResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
