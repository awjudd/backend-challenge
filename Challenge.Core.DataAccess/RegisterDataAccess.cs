using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Core.DataAccess;

public static class RegisterDataAccess
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChallengeDbContext>(
            options => options.UseSqlite()
        );
        services.AddSingleton(configuration);

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    public async static Task<IServiceProvider> ResetDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();

            if (context is not null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return serviceProvider;
    }
}