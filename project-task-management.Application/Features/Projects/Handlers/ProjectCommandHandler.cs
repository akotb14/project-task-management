using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Features.Projects.Command.Requests;
using project_task_management.Application.Features.Projects.Queries.Responses;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities;
using project_task_management.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnauthorizedAccessException = project_task_management.Domain.Exceptions.UnauthorizedAccessException;

namespace project_task_management.Application.Features.Projects.Handlers
{
    public class ProjectCommandHandler :IRequestHandler<CreateProjectRequest ,Response<string>>, 
        IRequestHandler<UpdateProjectRequest, Response<string>>, 
        IRequestHandler<DeleteProjectRequest, Response<string>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Handle(CreateProjectRequest request , CancellationToken CancellationToken)
        {
            var currentUser = _userRepository.GetUserId();

            var newProject = new Project
            {
                Name = request.Name,
                Description = request.Description,
                UserId = currentUser
            };
            var result = await _projectRepository.AddAsync(newProject);
            if(result == null)
            {
                throw new BadRequestException("Failed to create the project");
            }

            return new Response<string>(message: "Project created successfully.", statusCode: HttpStatusCode.Created);
                        
        }
        public async Task<Response<string>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _userRepository.GetUserId();

            var existProject = await _projectRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == currentUser , cancellationToken);
            if(existProject == null)
            {
                throw new UnauthorizedAccessException("Project not found or access denied");
            }
            existProject.Name = request.Name;
            existProject.Description  = request.Description;
            var result = await _projectRepository.UpdateAsync(existProject);
            if (result == null)
            {
                throw new BadRequestException("Project not found or access denied");
            }
            return new Response<string>(message: "Project updated successfully.", statusCode: HttpStatusCode.OK);
        }
        public async Task<Response<string>> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _userRepository.GetUserId();

            var existProject = await _projectRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == currentUser , cancellationToken);
            if (existProject == null)
            {
                throw new UnauthorizedAccessException("Project not found or access denied");
            }

            var result = await _projectRepository.DeleteAsync(existProject);
            if (result == null)
            {
                throw new BadRequestException("Project not found or access denied");
            }

            return new Response<string>(message: "Delete Project Successfully", statusCode: HttpStatusCode.OK);
        }
    }
}
