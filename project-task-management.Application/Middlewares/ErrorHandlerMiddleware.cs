using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using DbUpdateException = project_task_management.Domain.Exceptions.DbUpdateException;
using KeyNotFoundException = project_task_management.Domain.Exceptions.KeyNotFoundException;
using UnauthorizedAccessException = project_task_management.Domain.Exceptions.UnauthorizedAccessException;

namespace project_task_management.Application.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new Response<object>
        {
            Succeeded = false
        };

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                response.StatusCode = HttpStatusCode.UnprocessableEntity;
                response.Message = validationException.Message;
                response.Errors = response.Errors = validationException.Errors
                    .SelectMany(x => x.Value).ToList(); 
                break;

            case UnauthorizedAccessException unauthorizedException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Message = unauthorizedException.Message;

                break;

            case BadRequestException badRequestException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = badRequestException.Message;

                break;

            case NotFoundException keyNotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = keyNotFoundException.Message;

                break;

            case DbUpdateException dbUpdateException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = dbUpdateException.Message;

                break;

            case ApiException apiException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = apiException.Message;

                break;

            case ArgumentNullException argumentNullException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = argumentNullException.Message;

                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = exception.Message;

                break;
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}