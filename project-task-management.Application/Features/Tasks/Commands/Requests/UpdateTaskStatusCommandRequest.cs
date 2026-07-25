using MediatR;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Enums;


namespace project_task_management.Application.Features.Tasks.Commands.Requests
{
    public class UpdateTaskStatusCommandRequest : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
