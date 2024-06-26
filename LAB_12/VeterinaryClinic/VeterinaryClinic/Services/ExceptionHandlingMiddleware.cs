﻿using System.Net;
using System.Text.Json;
using VeterinaryClinic.DTO;

namespace VeterinaryClinic.Services
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An Error Occurred Processing The Request at {Path}", context.Request.Path);

            var responseDto = exception switch
            {
                ApplicationException _ => new ExceptionResponseDto(HttpStatusCode.BadRequest, "Application Exception Occurred."),
                KeyNotFoundException _ => new ExceptionResponseDto(HttpStatusCode.NotFound, "Requested Endpoint Was Not Found."),
                UnauthorizedAccessException _ => new ExceptionResponseDto(HttpStatusCode.Unauthorized, "Unauthorized Access."),
                _ => new ExceptionResponseDto(HttpStatusCode.InternalServerError, "Internal Server Error. Please Retry Later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseDto.StatusCode;

            try
            {
                var response = JsonSerializer.Serialize(new { error = responseDto.Message, statusCode = responseDto.StatusCode });
                await context.Response.WriteAsync(response);
            }
            catch (Exception serializeException)
            {
                _logger.LogError(serializeException, "An Error Occurred While Serializing The Error Response.");
            }
        }
    }
}