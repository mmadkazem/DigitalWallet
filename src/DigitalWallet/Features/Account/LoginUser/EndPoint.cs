namespace DigitalWallet.Features.Account.LoginUser;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapGet("/Login",
            async ([AsParameters] LoginUserQueryRequest request, IUserFacadeService _userFacade, CancellationToken cancellationToken) =>
            {
                var result = await _userFacade.LoginUser.Handle(request, cancellationToken);
                return Results.Ok(result);
            });
    }
}