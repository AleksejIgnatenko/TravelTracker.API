using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using TravelTracker.Core.Exceptions;

namespace TravelTracker.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                var statusCode = StatusCodes.Status400BadRequest;

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Errors });
                await context.Response.WriteAsync(result);
            }
            catch (DataRepositoryException ex)
            {
                var statusCode = (int)ex.HttpStatusCode;

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Database update error occurred");

                if (ex.InnerException is SqlException sqlEx && sqlEx.Message.Contains("FirstName cannot contain digits"))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { error = "FirstName cannot contain digits" });
                    await context.Response.WriteAsync(result);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { error = "Database update error occurred" });
                    await context.Response.WriteAsync(result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
