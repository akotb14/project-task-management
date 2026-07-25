using FluentValidation;
using project_task_management.Application.Features.Tasks.Commands.Requests;


namespace project_task_management.Application.Features.Tasks.Commands.Validators
{
    public class UpdateTaskCommandValidator :AbstractValidator<UpdateTaskCommandRequest>
    {
       public UpdateTaskCommandValidator() {
            RuleFor(x => x.ProjectId)
            .GreaterThan(0)
            .WithMessage("A valid project is required.");

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Task title is required.")
                .Must(title => !string.IsNullOrWhiteSpace(title))
                .WithMessage("Task title cannot be empty or whitespace.")
                .MinimumLength(3)
                .WithMessage("Task title must be at least 3 characters.")
                .MaximumLength(200)
                .WithMessage("Task title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Priority)
                .IsInEnum()
                .WithMessage("Invalid task priority.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");
        }
    }
}
