using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using Alpha.Shared.Exceptions;
using Alpha.Shared.Models;
using Fanex.Data;
using Microsoft.AspNetCore.Diagnostics;

namespace Alpha.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    private const string BreakLine = "\r\n";
    private const string TwoBreakLine = "\r\n\r\n";

    public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app, bool isDevelopment, Fanex.Logging.ILogger logger)
        => app.UseExceptionHandler(configure => configure.Run(context => LogError(context, isDevelopment, logger)));

    private static async Task LogError(HttpContext context, bool isDevelopment, Fanex.Logging.ILogger logger)
    {
        var response = context.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            var exception = contextFeature.Error;
            await logger.ErrorAsync(await BuildError(context.Request, exception), exception);

            var apiError = exception is DomainException domainException
                ? new ApiError(domainException)
                : new ApiError(response.StatusCode, $"An error occurred. {(isDevelopment ? exception.ToString() : string.Empty)})");

            await response.WriteAsync(JsonSerializer.Serialize(apiError));
        }
    }

    private static async Task<string> BuildError(HttpRequest request, Exception exception)
    {
        var errorBuilder = new StringBuilder();
        errorBuilder
            .Append(exception.Message).Append(TwoBreakLine)
            .Append("PATH: ")
            .Append($"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}")
            .Append(TwoBreakLine)
            .Append("BODY: ")
            .Append(await GetRawBodyStringAsync(request))
            .Append(TwoBreakLine)
            .Append("HEADERS:").Append(BreakLine)
            .Append(GetHeaderValues(request))
            .Append(TwoBreakLine)
            .Append("COOKIES:").Append(BreakLine)
            .Append(GetCookiesValues(request))
            .Append(TwoBreakLine);
        
        if (exception is ObjectDbException objectDbException
            && exception.InnerException is SqlException sqlException)
        {
            errorBuilder.Append(BuildDbException(objectDbException, sqlException));
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

    private static string GetHeaderValues(HttpRequest request)
        => string.Join(BreakLine, request.Headers.Select(header => $"{header.Key}:{header.Value}"));

    private static string GetCookiesValues(HttpRequest request)
        => string.Join(BreakLine, request.Cookies.Select(header => $"{header.Key}:{header.Value}"));

    private static string BuildDbException(ObjectDbException dbException, SqlException sqlException)
    {
        var errorBuilder = new StringBuilder();
        errorBuilder
            .Append("DATABASE INFO:").Append(BreakLine)
            .Append("Server: ").Append(sqlException.Server).Append(BreakLine)
            .Append("DbName: ").Append(dbException.DbName).Append(BreakLine)
            .Append("SpName: ").Append(dbException.SpName).Append(BreakLine)
            .Append("Parameters: ").Append(BreakLine).Append(GetSpParameters(dbException.Parameters)).Append(BreakLine)
            .Append("Line: ").Append(sqlException.LineNumber).Append(TwoBreakLine);

        return errorBuilder.ToString();
    }

    private static string GetSpParameters(IDictionary<string, object> parameters)
        => string.Join(BreakLine, parameters.Select(p => $"{p.Key}={p.Value}"));
}
