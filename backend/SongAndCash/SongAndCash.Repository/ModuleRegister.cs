using Microsoft.Extensions.DependencyInjection;

namespace SongAndCash.Repository;

public static class ModuleRegister
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecoverableSalesRepository, RecoverableSalesRepository>();
    }
}