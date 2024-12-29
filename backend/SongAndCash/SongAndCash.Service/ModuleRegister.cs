using Microsoft.Extensions.DependencyInjection;
using SongAndCash.Service.Mapper;

namespace SongAndCash.Service;

public static class ModuleRegister
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMapper, UserMapper>();
    }
}