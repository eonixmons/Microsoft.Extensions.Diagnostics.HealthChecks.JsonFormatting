A helper that integrates with ASP.net core health check system and formats output in JSON.

## Getting started

A Nuget package is available [here](https://www.nuget.org/packages/Eonix.Microsoft.Extensions.Diagnostics.HealthChecks.JsonFormatting). It can be installed using the Nuget package manager or the `dotnet` CLI.

`dotnet add Eonix.Microsoft.Extensions.Diagnostics.HealthChecks.JsonFormatting`

## Example

In your Program.cs file, use the extension method in your pipeline configuration: 

```csharp
var app = builder.Build();

//Code omitted for brevity

app.UseJsonFormattedHealthChecks();

//Code omitted for brevity

app.Run();
```

Then, when fetching `http://<your-site>/health`, you get the following output:

```json
{
  "status": "Healthy",
  "components": [
    {
      "component": "Database",
      "status": "Healthy",
      "description": null
    }
  ],
  "duration": "00:00:01.1241411"
}
```
