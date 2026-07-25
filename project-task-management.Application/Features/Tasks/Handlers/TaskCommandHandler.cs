using MediatR;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Features.Tasks.Commands.Requests;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Enums;
using project_task_management.Domain.Exceptions;

using System.Net;
using UnauthorizedAccessException = project_task_management.Domain.Exceptions.UnauthorizedAccessException;

namespace project_task_management.Application.Features.Tasks.Handlers
{
    public class TaskCommandHandler :IRequestHandler<CreateTaskCommandRequest , Response<string>>,
        IRequestHandler<UpdateTaskCommandRequest, Response<string>>,
        IRequestHandler<UpdateTaskStatusCommandRequest, Response<string>>,
        IRequestHandler<DeleteTaskCommandRequest, Response<string>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskCommandHandler(IProjectRepository projectRepository, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Handle(CreateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _userRepository.GetUserId();

            var projectExists = await _projectRepository
                .GetTableNoTracking()
                .AnyAsync(
                    p => p.Id == request.ProjectId &&
                         p.UserId == userId,
                    cancellationToken);

            if (!projectExists)
            {
                throw new NotFoundException("Project not found.");
            }

            var task = new Domain.Entities.Task
            {
                ProjectId = request.ProjectId,
                Title = request.Title.Trim(),
                Description = request.Description?.Trim() ?? string.Empty,
                DueDate = request.DueDate,
                Status = TaskStatusEnum.Pending,
                Priority = request.Priority
            };

            await _taskRepository.AddAsync(task);

            return new Response<string>(
                message: "Task created successfully.",
                statusCode: HttpStatusCode.Created);
        }



        public async Task<Response<string>> Handle(UpdateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _userRepository.GetUserId();
            var task = await _taskRepository
                .GetTableAsTracking()
                .FirstOrDefaultAsync(
                    t => t.Id == request.Id &&
                         t.ProjectId == request.ProjectId &&
                         t.Project.UserId == userId,
                    cancellationToken);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.Priority = request.Priority;

            await _taskRepository.UpdateAsync(task);

            return new Response<string>(
                message: "Task updated successfully.",
                statusCode: HttpStatusCode.OK);
        }



        public async Task<Response<string>> Handle(UpdateTaskStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _userRepository.GetUserId();
            var task = await _taskRepository
       .GetTableAsTracking()
       .FirstOrDefaultAsync(
           t => t.Id == request.Id &&
                t.ProjectId == request.ProjectId &&
                t.Project.UserId == userId,
           cancellationToken);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            task.Status = request.Status;

            await _taskRepository.UpdateAsync(task);

            return new Response<string>(
                message: "Task status updated successfully.",
                statusCode: HttpStatusCode.OK);
        }



        public async Task<Response<string>> Handle(DeleteTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _userRepository.GetUserId();
            var task = await _taskRepository
                .GetTableAsTracking()
                .FirstOrDefaultAsync(
                    t => t.Id == request.Id &&
                         t.ProjectId == request.ProjectId &&
                         t.Project.UserId == userId,
                    cancellationToken);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            await _taskRepository.DeleteAsync(task);

            return new Response<string>(
                message: "Task deleted successfully.",
                statusCode: HttpStatusCode.OK);
        }
    }
}
