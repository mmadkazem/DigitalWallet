namespace DigitalWallet.Features.Account.LoginByRefreshToken;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapGet("/LoginByRefreshToken",
            [Authorize(AuthenticationSchemes = "RefreshScheme")] async (IUserFacadeService _userFacade, CancellationToken cancellationToken, HttpContext context) =>
            {
                var userId = context.User.UserId();
                var result = await _userFacade.LoginByRefreshToken.Handle(userId, cancellationToken);
                return Results.Ok(result);
            });
    }
}