using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using Alpha.Shared.Exceptions;
using Fanex.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app, bool isDevelopment, Fanex.Logging.ILogger logger)
        => app.UseExceptionHandler(configure => configure.Run(context => LogErrorAsync(context, isDevelopment, logger)));

    private static async Task LogErrorAsync(HttpContext context, bool isDevelopment, Fanex.Logging.ILogger logger)
    {
        var response = context.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            var exception = contextFeature.Error;
            await logger.ErrorAsync(await BuildErrorAsync(context.Request, exception), exception);

            var problemDetails = exception is DomainException domainException
                ? new ProblemDetails
                {
                    Status = domainException.StatusCode,
                    Detail = domainException.Message
                }
                : new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = $"An error occurred. {(isDevelopment ? exception.ToString() : string.Empty)}"
                };

            await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }

    private static async Task<string> BuildErrorAsync(HttpRequest request, Exception exception)
    {
        const string OneBreakLine = "\r\n";
        const string TwoBreakLine = "\r\n\r\n";

        var errorBuilder = new StringBuilder();
        errorBuilder
            .Append(exception.Message).Append(TwoBreakLine)
            .Append("PATH: ")
            .Append($"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}")
            .Append(TwoBreakLine)
            .Append("BODY: ")
            .Append(await GetRawBodyStringAsync(request))
            .Append(TwoBreakLine)
            .Append("HEADERS:").Append(OneBreakLine)
            .Append(GetHeaderValues(request, OneBreakLine))
            .Append(TwoBreakLine)
            .Append("COOKIES:").Append(OneBreakLine)
            .Append(GetCookiesValues(request, OneBreakLine))
            .Append(TwoBreakLine);
        
        if (exception is ObjectDbException objectDbException
            && exception.InnerException is SqlException sqlException)
        {
            errorBuilder
                .Append(BuildDbException(objectDbException, sqlException, OneBreakLine))
                .Append(TwoBreakLine);
        }

        errorBuilder
            .Append("EXCEPTION: ")
            .Append(exception.ToString());

        return errorBuilder.ToString();
    }

    private static async Task<string> GetRawBodyStringAsync(HttpRequest request)
    {
        using (var reader = new StreamReader(request.Body, Encoding.UTF8))
        {
            return await reader.ReadToEndAsync();
        }
    }

    private static string GetHeaderValues(HttpRequest request, string breakLine)
        => string.Join(breakLine, request.Headers.Select(header => $"{header.Key}:{header.Value}"));

    private static string GetCookiesValues(HttpRequest request, string breakLine)
        => string.Join(breakLine, request.Cookies.Select(header => $"{header.Key}:{header.Value}"));

    private static string BuildDbException(ObjectDbException dbException, SqlException sqlException, string breakLine)
    {
        var errorBuilder = new StringBuilder();
        errorBuilder
            .Append("DATABASE INFO:").Append(breakLine)
            .Append("Server: ").Append(sqlException.Server).Append(breakLine)
            .Append("DbName: ").Append(dbException.DbName).Append(breakLine)
            .Append("SpName: ").Append(dbException.SpName).Append(breakLine)
            .Append("Parameters: ").Append(breakLine).Append(GetSpParameters(dbException.Parameters, breakLine)).Append(breakLine)
            .Append("Line: ").Append(sqlException.LineNumber);

        return errorBuilder.ToString();
    }

    private static string GetSpParameters(IDictionary<string, object> parameters, string breakLine)
        => string.Join(breakLine, parameters.Select(p => $"{p.Key}={p.Value}"));
}
