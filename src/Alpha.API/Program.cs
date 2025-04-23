using Alpha.API.Extensions;
using Alpha.Customers.Extensions;
using Alpha.Reports.Extensions;
using Alpha.Shared.Extensions;
using Fanex.Data.Repository;
using Fanex.Data.SqlServer;
using Fanex.Logging;
using MediatR;

var assembly = typeof(Program).Assembly;
var assemblyName = assembly.GetName().Name;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services
    .AddHttpContextAccessor()
    .AddFanexSql(configuration)
    .AddSingleton<IDynamicRepository, DynamicRepository>()
    .AddRemotingRef(configuration)
    .AddFanexLogging(configuration)
    .AddFanexCachingMemory()
    .AddMediatR(assembly)
    //.AddCustomLocalization()
    .AddSharedServices()
    .AddCustomerServices(configuration)
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
    .ConfigureExceptionHandler(isDevelopment, Logger.Log);

app.MapGet("/", () => $"{assemblyName} is running.");

app.MapControllers();
await Logger.Log.InfoAsync($"{assemblyName} has been started up.");

await app.RunAsync();
