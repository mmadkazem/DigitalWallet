namespace DigitalWallet.Features.Wallet.GetTransactions;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGroup(WalletFeatureManager.PrefixTransaction)
            .WithTags(WalletFeatureManager.EndpointTagNameTransaction)
            .MapGet("/{WalletId}",
            async ([AsParameters] GetTransactionsQueryRequest request, IWalletFacadeService _walletFacade, CancellationToken cancellationToken) =>
            {
                var results = await _walletFacade.GetTransactions.Handle(request, cancellationToken);
                return Results.Ok(results);
            }).RequireAuthorization();
    }
}