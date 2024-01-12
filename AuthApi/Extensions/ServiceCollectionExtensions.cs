using AuthenticationBroker.TokenHandler;
using AuthService.Services;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.Context.Repositories.Auth;
using DatabaseBroker.Context.Repositories.Structures;
using DatabaseBroker.Context.Repositories.Users;
using DatabaseBroker.Repositories.Auth;
using DatabaseBroker.Repositories.Token;
using Entity.Models;
using RoleService.Service;
using UserService.Service;

namespace AuthApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OneIDOptions>(configuration
            .GetSection("OneIDOptions"));
        return services;
    }
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<HttpClient, HttpClient>();
        services.AddScoped<IAuthService, AuthService.Services.AuthService>();
        services.AddScoped<IUserService, UserService.Service.UserService>();
        services.AddScoped<IRoleService, RoleService.Service.RoleService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStructureRepository, Structurepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IStructurePermissionRepository, StructurePermissionRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<ISignMethodsRepository, SignMethodsRepository>();
        services.AddScoped<IUserCertificateRepository, UserCertificateRepository>();


        return services;
    }
}