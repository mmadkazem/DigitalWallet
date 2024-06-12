using DigitalWallet.Features.Account.LoginByRefreshToken;

namespace DigitalWallet.Features.Account;

public abstract class FeatureManager
{
    public const string EndpointTagName = "Account";
    public const string Prefix = "/Account";

    public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection service)
        {
            // DI Facade
            service.AddScoped<IUserFacadeService, UserFacadeService>();

            // DI Services
            service.AddScoped<ILoginByRefreshTokenQueryHandler, LoginByRefreshTokenQueryHandler>();
            service.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
            service.AddScoped<ILoginUserQueryHandler, LoginUserQueryHandler>();

            // DI External Services
            service.AddScoped<ITokenFactoryService, TokenFactoryService>();
        }
    }
}