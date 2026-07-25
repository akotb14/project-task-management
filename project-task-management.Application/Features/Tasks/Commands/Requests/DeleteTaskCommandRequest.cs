using MediatR;
using project_task_management.Application.ResultHandler;


namespace project_task_management.Application.Features.Tasks.Commands.Requests
{
    public class DeleteTaskCommandRequest : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

    }
}
