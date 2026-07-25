using FluentValidation;
using project_task_management.Application.Features.Authentication.Commands.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Authentication.Commands.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
