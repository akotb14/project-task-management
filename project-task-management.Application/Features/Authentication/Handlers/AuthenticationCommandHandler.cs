using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Features.Authentication.Commands.Requests;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.Interface.Service;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities.Identity;
using project_task_management.Domain.Exceptions;
using System.Net;


namespace project_task_management.Application.Features.Authentication.Handlers
{
    public class AuthenticationCommandHandler : IRequestHandler<RegisterCommandRequest , Response<string>> ,IRequestHandler<LoginCommandRequest, Response<AuthJwtResult>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;
        public AuthenticationCommandHandler(UserManager<User> userManager, IJwtService jwtService, IIdentityService identityService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var existUserEmail = await _userManager.FindByEmailAsync(request.Email);
            var existUserName = await _userManager.FindByNameAsync(request.Email);
            if (existUserEmail != null || existUserName != null)
            {
                throw new BadRequestException("email or user name is already exist");
            }
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user , request.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return new Response<string> { Message = "Registration completed successfully.", Succeeded=true , StatusCode = HttpStatusCode.Created};
        }

        public async Task<Response<AuthJwtResult>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email); ;
            if(user == null)
            {
                throw new BadRequestException("Email or password is incorrect.");
            }
            var checkPassword = await _identityService.CheckPasswordAsync(user, request.Password, false);
            if (!checkPassword) 
            { 
                throw new BadRequestException("Email or password is incorrect."); 
            }
           var accessToken=  await _jwtService.GetJWTToken(user);
            return new Response<AuthJwtResult> { Message = "Login successful.", Data = accessToken ,Succeeded=true, StatusCode = HttpStatusCode.OK };
        }


    }


}
