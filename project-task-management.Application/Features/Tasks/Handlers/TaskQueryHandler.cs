using MediatR;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Features.Tasks.Commands.Requests;
using project_task_management.Application.Features.Tasks.Queries.Requests;
using project_task_management.Application.Features.Tasks.Queries.Responses;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Features.Tasks.Handlers
{
    public class TaskQueryHandler:IRequestHandler<GetTaskByProjectByUserQueryRequests , Response<List<GetTasksByProjectByUserQueryResponse>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskQueryHandler(IProjectRepository projectRepository, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<List<GetTasksByProjectByUserQueryResponse>>> Handle(GetTaskByProjectByUserQueryRequests request, CancellationToken cancellationToken)
        {
            var userId = _userRepository.GetUserId();
            var existProject = await _projectRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == request.ProjectId);
            if (existProject == null)
            {
                throw new NotFoundException("Project not found");
            }
            var tasksByProject = await _taskRepository.GetTableNoTracking()
                .Where(e=>e.ProjectId == existProject.Id)
                .Select(e=>new GetTasksByProjectByUserQueryResponse
                {
                    Id = e.Id,
                    ProjectId = e.ProjectId,
                    ProjectName = e.Project.Name,
                    Title = e.Title,
                    Description = e.Description,
                    Priority = e.Priority,
                    Status = e.Status,
                    DueDate = e.DueDate,
                }).ToListAsync(cancellationToken);

            return new Response<List<GetTasksByProjectByUserQueryResponse>>(tasksByProject);
        }

    }
}
