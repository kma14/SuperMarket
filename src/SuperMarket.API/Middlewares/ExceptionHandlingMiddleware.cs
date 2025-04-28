using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarket.API.Middlewares;

public static class ExceptionHandlingMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionFeature?.Error;
                var traceId = context.TraceIdentifier;

                logger.LogError(exception, "Unhandled exception (TraceId: {TraceId})", traceId);

                var problem = new ProblemDetails
                {
                    Title = "An unexpected error occurred!",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = exception?.Message,
                    Instance = context.Request.Path,
                    Type = "https://supermarket.com/errors/unhandledErrors"
                };

                problem.Extensions["traceId"] = traceId;

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problem.Status.Value;
                await context.Response.WriteAsJsonAsync(problem);
            });
        });
    }
}
