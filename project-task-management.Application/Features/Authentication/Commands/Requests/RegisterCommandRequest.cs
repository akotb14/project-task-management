using MediatR;
using project_task_management.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Authentication.Commands.Requests
{
    public class RegisterCommandRequest :IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
