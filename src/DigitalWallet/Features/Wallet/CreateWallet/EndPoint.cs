namespace DigitalWallet.Features.Wallet.CreateWallet;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app
            .MapGroup(WalletFeatureManager.Prefix)
            .WithTags(WalletFeatureManager.EndpointTagName)
            .MapPost("/",
            async ([FromBody] CreateWalletCommandRequest request, IWalletFacadeService _walletFacade, CancellationToken cancellationToken) =>
            {
                await _walletFacade.CreateWallet.Handle(request, cancellationToken);
                return Results.Ok();
            })
            .Validator<CreateWalletCommandRequest>()
            .RequireAuthorization();
    }
}