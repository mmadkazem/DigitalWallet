namespace DigitalWallet.Features.Account.CreateUser;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPost("/Register",
            async ([FromBody] CreateUserCommandRequest request, IUserFacadeService _userFacade, CancellationToken cancellationToken) =>
            {
                await _userFacade.CreateUser.Handle(request, cancellationToken);
                return Results.Ok();
            }).Validator<CreateUserCommandRequest>();
    }
}