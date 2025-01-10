﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using BuildingBlocks.Exceptions;

namespace BuildingBlocks.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
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
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var result = "Something went wrong";
        switch (exception)
        {
            case ValidationException validationException:
                code = StatusCodes.Status400BadRequest;
                result = JsonSerializer.Serialize(validationException.Message);
                _logger.LogError(exception, "ValidationException was occured");
                break;
            case NotFoundException:
                code = StatusCodes.Status404NotFound;
                _logger.LogError(exception, "NotFoundException was occured");
                break;
            case ConflictException conflictException:
                code = StatusCodes.Status409Conflict;
                result = JsonSerializer.Serialize(conflictException.Message);
                _logger.LogError(exception, "ConflictException was occured");
                break;
            case OperationCanceledException canceledException:
                code = StatusCodes.Status499ClientClosedRequest;
                result = JsonSerializer.Serialize(canceledException.Message);
                _logger.LogError(exception, "Operation was canceled");
                break;
            default:
                _logger.LogError(exception, "An unexpected error occurred");
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}
