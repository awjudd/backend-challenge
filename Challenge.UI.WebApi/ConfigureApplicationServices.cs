using Challenge.Core.DataAccess;

namespace Challenge.UI.WebApi;

public static class ConfigureApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataPersistence(configuration);

        return services;
    }
}