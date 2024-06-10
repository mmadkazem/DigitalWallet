namespace DigitalWallet.Features.Wallet.AddBlockBalance;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app
            .MapGroup(WalletFeatureManager.Prefix)
            .WithTags(WalletFeatureManager.EndpointTagName)
            .MapPut("/",
            async ([FromBody] AddBlockBalanceCommandRequest request, IWalletFacadeService _walletFacade, CancellationToken cancellationToken) =>
            {
                await _walletFacade.AddBlockBalance.Handle(request, cancellationToken);
                return Results.Ok();
            })
            .Validator<AddBlockBalanceCommandRequest>()
            .RequireAuthorization();
    }
}