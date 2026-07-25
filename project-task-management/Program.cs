using Microsoft.EntityFrameworkCore;
using project_task_management.Application;
using project_task_management.Application.Middlewares;
using project_task_management.Infrastructure;
using project_task_management.Infrastructure.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(e=>e.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));

//dependency of Infrastructure 
builder.Services.AddModuleInfrastructureDependencies(builder.Configuration).AddModuleApplicationDependencies();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();


app.Run();
