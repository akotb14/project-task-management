using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Application.Interface.Service
{
    public interface IIdentityService
    {
        Task<bool> CheckPasswordAsync(User user, string password, bool v);
    }
}
