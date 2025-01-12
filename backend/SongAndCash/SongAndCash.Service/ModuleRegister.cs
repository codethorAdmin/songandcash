using Microsoft.Extensions.DependencyInjection;
using SongAndCash.Service.Business;
using SongAndCash.Service.Mapper;

namespace SongAndCash.Service;

public static class ModuleRegister
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRecoverableSalesService, RecoverableSalesService>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IRecoverableSalesMapper, RecoverableSalesMapper>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IContractMapper, ContractMapper>();
        services.AddScoped<IEmailClient, EmailClient>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}
