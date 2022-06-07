using Castle.Windsor.MsDependencyInjection;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryExample.Api2;
using OpenTelemetryExample.Core;
using OpenTelemetryExample.Core.IoC;
using System.Diagnostics;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;
Activity.ForceDefaultIdFormat = true;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddOpenTelemetryTracing(config => config
    .AddSource("OpenTelemetryExample.Api2")
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: "OpenTelemetryExample.Api2", serviceVersion: "1.0"))
    .AddConsoleExporter()
    .AddJaegerExporter(c =>
    {
        c.AgentHost = "localhost";
        c.AgentPort = 6831;
    })
    .AddZipkinExporter(c =>
    {
        c.Endpoint = new Uri("http://localhost:9412/api/v2/spans");
    })
    .AddAspNetCoreInstrumentation(options =>
    {
        options.Filter = (req) => !req.Request.Path.ToUriComponent().Contains("index.html", StringComparison.OrdinalIgnoreCase) &&
            !req.Request.Path.ToUriComponent().Contains("swagger", StringComparison.OrdinalIgnoreCase);
    })
    .AddHttpClientInstrumentation()
    .AddSqlClientInstrumentation((options => {
        options.SetDbStatementForText = true;
        options.RecordException = true;
    })));

builder.Services
    .AddOpenTelemetryMetrics(builder => builder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OpenTelemetryExample.Api2"))
        .AddMeter()
        .AddRuntimeMetrics()
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter());

builder.Host
    .ConfigureLogging(logging => logging
        .ClearProviders()
        .AddOpenTelemetry(options =>
        {
            // Export the body of the message
            options.IncludeFormattedMessage = true;
            // Configure the resource attribute `service.name` to MyServiceName
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OpenTelemetryExample.Api2"));
            options.AddConsoleExporter();
        }));

builder.Services.AddSingleton(TracerProvider.Default.GetTracer("OpenTelemetryExample.Api2"));

WindsorRegistrationHelper.AddServices(ContainerManager.WindsorContainer, builder.Services);

BootStrapper.InitializeContainer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
