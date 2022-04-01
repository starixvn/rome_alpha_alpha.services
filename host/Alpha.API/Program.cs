using System.Reflection;
using Alpha.API.Extensions;
using Alpha.Customers.Extensions;
using Alpha.Notifications.Extensions;
using Alpha.Reports.Extensions;
using Alpha.Shared.Extensions;
using Alpha.Shared.Infrastructure;
using Fanex.Logging;
using MediatR;

var assembly = Assembly.GetExecutingAssembly();
var assemblyName = assembly.GetName().Name;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services
    .AddFanexDataWithSqlServer(configuration)
    .AddRemotingRef(configuration)
    .AddFanexLogging(configuration)
    .AddFanexCachingMemory()
    .AddMediatR(assembly)
    //.AddCustomLocalization()
    .AddHMACAuthentication(configuration)
    .AddSharedServices()
    .AddCustomerServices(configuration)
    .AddNotificationServices(configuration)
    .AddReportServices(configuration)
    .AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

var app = builder.Build();
var isDevelopment = app.Environment.IsDevelopment();

if (!isDevelopment)
{
    builder.WebHost.UseUrls("http://0.0.0.0:5000");
}

// Configure the HTTP request pipeline.
app.UseSwagger().UseSwaggerUI();

app
    .ConfigureExceptionHandler(isDevelopment, Logger.Log)
    .UseHttpsRedirection()
    .UseAuthorization();
    //.UseCustomLocalization();

app.MapGet("/", () => $"{assemblyName} is running.");

app.MapControllers();

await Logger.Log.InfoAsync($"{assemblyName} has been started up.");

await app.RunAsync();
