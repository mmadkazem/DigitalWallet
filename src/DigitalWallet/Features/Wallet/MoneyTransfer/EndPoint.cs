namespace DigitalWallet.Features.Wallet.MoneyTransfer;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGroup(WalletFeatureManager.PrefixTransaction)
            .WithTags(WalletFeatureManager.EndpointTagNameTransaction)
            .MapPost("/",
            async ([FromBody] MoneyTransferCommandRequest request, IWalletFacadeService _walletFacade, CancellationToken cancellationToken) =>
            {
                await _walletFacade.MoneyTransfer.Handle(request, cancellationToken);
                return Results.Ok();
            }).RequireAuthorization();
    }
}