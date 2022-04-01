using Alpha.Customers.Configurations;
using Alpha.Customers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Customers.Extensions;

public static class CustomerDIExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<CustomerAppSettings>(configuration.GetSection(nameof(CustomerAppSettings)))
            .AddScoped<ICustomerService, CustomerService>();
}
