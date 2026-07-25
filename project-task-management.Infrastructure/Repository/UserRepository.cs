using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using project_task_management.Application.Interface.Repository;
using project_task_management.Domain.Entities.Identity;
using project_task_management.Domain.Helper;
using project_task_management.Infrastructure.Context;

using UnauthorizedAccessException = project_task_management.Domain.Exceptions.UnauthorizedAccessException;

namespace project_task_management.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User> , IUserRepository  
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserManager<User> GetUserManager()
        {
            return _userManager;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(claims => claims.Type == nameof(UserClaimModel.Id))?.Value;
            if (userId == null ) {
                throw new UnauthorizedAccessException("User not Authorized");
            }
            Console.WriteLine(userId);
            return userId;
        }
    }
}
