using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Test.Services;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 

        if (exception is NotFoundException) code = HttpStatusCode.NotFound; // 404
        else if (exception is BadRequestException) code = HttpStatusCode.BadRequest; // 400

        var result = JsonSerializer.Serialize(new ProblemDetails
        {
            Status = (int)code,
            Title = "An error occurred while processing your request.",
            Detail = exception.Message
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
