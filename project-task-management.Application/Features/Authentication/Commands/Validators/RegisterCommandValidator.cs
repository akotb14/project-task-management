using FluentValidation;
using project_task_management.Application.Features.Authentication.Commands.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Authentication.Commands.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator() { ApplyValidationsRules(); }
        private void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                      .Matches(@"^[a-zA-Z0-9_]+$")
                      .WithMessage("Username may contain only letters, numbers, and underscores.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.").MinimumLength(5)
                .MaximumLength(100).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.").MinimumLength(5)
                .MaximumLength(100).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage("Invalid Egyptian phone number.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        }

    }
}
