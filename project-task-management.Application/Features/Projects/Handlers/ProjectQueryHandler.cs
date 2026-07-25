using MediatR;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Features.Projects.Queries.Requests;
using project_task_management.Application.Features.Projects.Queries.Responses;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities;
using project_task_management.Domain.Exceptions;


namespace project_task_management.Application.Features.Projects.Handlers
{
    public class ProjectQueryHandler : IRequestHandler<GetProductsByUserQuery , Response<List<GetProjectResponse>>>,
        IRequestHandler<GetProductByIdByUserQuery , Response<GetProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectQueryHandler(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<List<GetProjectResponse>>> Handle(GetProductsByUserQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _userRepository.GetUserId();
            var products = await _projectRepository.GetTableNoTracking()
                .Where(e => e.UserId == currentUser)
                .Include(e=>e.User)
                .Select(e=>new GetProjectResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreateAt = e.CreatedAt,
                UserId = e.UserId,
                FullName= $"{e.User.FirstName} {e.User.LastName}"  ,

            }).ToListAsync(cancellationToken);
            return new Response<List<GetProjectResponse>>(products) ; 
        }

        public async Task<Response<GetProjectResponse>> Handle(GetProductByIdByUserQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _userRepository.GetUserId();
            var product = await _projectRepository.GetTableNoTracking()
                .Where(e => e.Id == query.Id && e.UserId == currentUser)
                .Select(e => new GetProjectResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreateAt = e.CreatedAt,
                Task = e.Tasks.Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Priority = t.Priority,
                    DueDate = t.DueDate
                }).ToList(),
                UserId = currentUser,
                FullName = $"{e.User.FirstName} {e.User.LastName}",

            }).FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Project not found.");
            }
            return new Response<GetProjectResponse>(product) ;

        }
    }
}
