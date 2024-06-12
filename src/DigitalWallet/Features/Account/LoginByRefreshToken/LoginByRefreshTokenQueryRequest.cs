namespace DigitalWallet.Features.Account.LoginByRefreshToken;


public record LoginByRefreshTokenQueryRequest(Guid UserId)
{
    public static implicit operator LoginByRefreshTokenQueryRequest(Guid userId)
        => new(userId);
}


public interface ILoginByRefreshTokenQueryHandler
{
    Task<JwtTokenData> Handle(LoginByRefreshTokenQueryRequest request, CancellationToken cancellationToken);
}
public sealed class LoginByRefreshTokenQueryHandler(AppDbContext context, ITokenFactoryService tokenFactory) : ILoginByRefreshTokenQueryHandler
{
    private readonly AppDbContext _context = context;
    private readonly ITokenFactoryService _tokenFactory = tokenFactory;

    public async Task<JwtTokenData> Handle(LoginByRefreshTokenQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
        ?? throw new UserNotFoundException();

        return _tokenFactory.CreateJwtTokens(user);
    }
}