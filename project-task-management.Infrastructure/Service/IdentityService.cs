using Azure.Core;
using Microsoft.AspNetCore.Identity;
using project_task_management.Application.Interface.Service;
using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<User> _signInManager;
        public IdentityService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password , bool lookout)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lookout);

            return result.Succeeded;
        }


    }
}
