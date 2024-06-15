namespace DigitalWallet.Features.Wallet.CreateWallet;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app
            .MapGroup(WalletFeatureManager.Prefix)
            .WithTags(WalletFeatureManager.EndpointTagName)
            .MapPost("/",
            async (CreateWalletDTO model, IWalletFacadeService _walletFacade, HttpContext context, CancellationToken cancellationToken) =>
            {
                var userId = context.User.UserId();
                await _walletFacade.CreateWallet.Handle(new(userId, model), cancellationToken);
                return Results.Ok();
            })
            .Validator<CreateWalletDTO>()
            .RequireAuthorization();
    }
}