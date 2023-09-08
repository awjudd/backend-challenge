using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Challenge.Core.DataAccess;

public static class RegisterDataAccess
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChallengeDbContext>(
            options => options.UseSqlite()
        );   
        
        return services;
    }
}