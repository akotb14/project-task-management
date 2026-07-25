using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using project_task_management.Application.Behaviors;
using project_task_management.Application.Features.Authentication.Commands.Requests;
using project_task_management.Application.Features.Authentication.Commands.Validators;
using project_task_management.Application.Interface.Service;
using System.Reflection;


namespace project_task_management.Application
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddModuleApplicationDependencies(this IServiceCollection services) {


            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterCommandRequest).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(ModuleApplicationDependencies).Assembly);
            return services;
        }
    }
}
