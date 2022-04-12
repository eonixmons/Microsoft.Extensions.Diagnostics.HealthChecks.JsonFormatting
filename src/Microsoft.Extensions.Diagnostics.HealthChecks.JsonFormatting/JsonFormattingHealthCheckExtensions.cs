using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.Diagnostics.HealthChecks.JsonFormatting;

public static class JsonFormattingHealthCheckExtensions
{
    /// <summary>
    /// Adds a middleware that provides JSON-formatted health check status
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="path">The path on which to provide health check status.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseJsonFormattedHealthChecks(this IApplicationBuilder app, string path = "/health")
    {
        app.UseHealthChecks(path, new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(BuildResponseBody(report));
            }
        });
        return app;
    }

    private static string BuildResponseBody(HealthReport report)
    {
        var body = new
        {
            status = report.Status.ToString(),
            components = report.Entries.Select(e => new
            {
                component = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            }),
            duration = report.TotalDuration
        };
        var serialized = JsonSerializer.Serialize(body, new JsonSerializerOptions {WriteIndented = true});
        return serialized;
    }
}