namespace DigitalWallet.Features.Wallet.GetWallet;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGroup(WalletFeatureManager.Prefix)
            .WithTags(WalletFeatureManager.EndpointTagName)
            .MapGet("/{Id}",
            async ([AsParameters] GetWalletQueryRequest request, IWalletFacadeService _walletFacade, CancellationToken cancellationToken) =>
            {
                var results = await _walletFacade.GetWallet.Handle(request, cancellationToken);
                return Results.Ok(results);
            }).RequireAuthorization();
    }
}