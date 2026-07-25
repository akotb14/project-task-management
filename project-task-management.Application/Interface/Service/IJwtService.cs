using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace project_task_management.Application.Interface.Service
{
    public interface IJwtService
    {
        Task<AuthJwtResult> GetJWTToken(User user);
        JwtSecurityToken ReadAccessJWTToken(string accessToken);
        bool ValidateToken(string accessToken);
    }
}
