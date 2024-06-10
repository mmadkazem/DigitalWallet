namespace DigitalWallet.Features.Wallet;

public abstract class WalletFeatureManager
{
    public const string EndpointTagName = "Wallet";
    public const string EndpointTagNameTransaction = "Transaction";
    public const string Prefix = "/Wallets";
    public const string PrefixTransaction = "/Transactions";

    public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection service)
        {
            // DI Facade
            service.AddScoped<IAddBlockBalanceCommandHandler, AddBlockBalanceCommandHandler>();
            service.AddScoped<IMoneyTransferCommandHandler, MoneyTransferCommandHandler>();
            service.AddScoped<IGetTransactionsQueryHandler, GetTransactionsQueryHandler>();
            service.AddScoped<IGetWalletQueryHandler, GetWalletQueryHandler>();
            service.AddScoped<IWalletFacadeService, WalletFacadeService>();

            // DI Services
            service.AddScoped<ICreateWalletCommandHandler, CreateWalletCommandHandler>();
        }
    }
}