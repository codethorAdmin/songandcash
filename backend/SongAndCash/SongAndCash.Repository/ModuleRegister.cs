using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SongAndCash.Repository;

public static class ModuleRegister
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IRecoverableSalesRepository, RecoverableSalesRepository>();

        services.AddDbContext<SongAndCashContext>(options =>
            options.UseMySql(
                "Server=localhost;Port=3306;Database=songandcash;User ID=root;Password=ABCabc123.;",
                new MySqlServerVersion(new Version(9, 0, 1))
            )
        );
    }
}
