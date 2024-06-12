using DigitalWallet.Features.Account.LoginByRefreshToken;

namespace DigitalWallet.Features.Account.Common;


public interface IUserFacadeService
{
    ICreateUserCommandHandler CreateUser { get; }
    ILoginUserQueryHandler LoginUser { get; }
    ILoginByRefreshTokenQueryHandler LoginByRefreshToken { get; }
}

public class UserFacadeService
    (AppDbContext context,
    ICreateUserCommandHandler createUser,
    ILoginUserQueryHandler loginUser,
    ILoginByRefreshTokenQueryHandler loginByRefresh)
: IUserFacadeService
{
    private readonly AppDbContext _context = context;
    private readonly ICreateUserCommandHandler _createUser = createUser;
    public ICreateUserCommandHandler CreateUser
        => _createUser;

    private readonly ILoginUserQueryHandler _loginUser = loginUser;
    public ILoginUserQueryHandler LoginUser
        => _loginUser;

    private readonly ILoginByRefreshTokenQueryHandler _loginByRefresh = loginByRefresh;
    public ILoginByRefreshTokenQueryHandler LoginByRefreshToken
        => _loginByRefresh;
}