namespace DigitalWallet.Features.Account.LoginUser;


public record LoginUserQueryRequest(string UserName, string Password);

public interface ILoginUserQueryHandler
{
    Task<JwtTokenData> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken = default);
}
public sealed class LoginUserQueryHandler(AppDbContext context, ITokenFactoryService tokenFactory) : ILoginUserQueryHandler
{
    private readonly AppDbContext _context = context;
    private readonly ITokenFactoryService _tokenFactory = tokenFactory;

    public async Task<JwtTokenData> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.AsQueryable()
                        .AsQueryable()
                        .AsNoTracking()
                        .Where(u => u.UserName == request.UserName && u.Password == request.Password)
                        .FirstOrDefaultAsync(cancellationToken)
        ?? throw new InValidUserNamePasswordException();

        return _tokenFactory.CreateJwtTokens(user);
    }
}