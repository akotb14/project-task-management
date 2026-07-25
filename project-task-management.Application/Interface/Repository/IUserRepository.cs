using Microsoft.AspNetCore.Identity;
using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Interface.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        UserManager<User> GetUserManager();
        string GetUserId();

    }
}
