namespace DigitalWallet.Features.Account.Common;


public interface IUserFacadeService
{
    ICreateUserCommandHandler CreateUser { get; }
    ILoginUserQueryHandler LoginUser { get; }
}

public class UserFacadeService
    (AppDbContext context,
    ICreateUserCommandHandler createUser,
    ILoginUserQueryHandler loginUser)
: IUserFacadeService
{
    private readonly AppDbContext _context = context;
    private readonly ICreateUserCommandHandler _createUser = createUser;
    public ICreateUserCommandHandler CreateUser
        => _createUser;

    private readonly ILoginUserQueryHandler _loginUser = loginUser;
    public ILoginUserQueryHandler LoginUser
        => _loginUser;
}