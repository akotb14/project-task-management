using MediatR;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Enums;


namespace project_task_management.Application.Features.Tasks.Commands.Requests
{
    public class UpdateTaskCommandRequest : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriorityEnum Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
