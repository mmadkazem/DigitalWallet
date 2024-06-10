namespace DigitalWallet.Features.Account.CreateUser;
public record CreateUserCommandRequest(string UserName, string Password);

public interface ICreateUserCommandHandler
{
    Task Handle(CreateUserCommandRequest request, CancellationToken cancellationToken = default);
}
public sealed class CreateUserCommandHandler(AppDbContext context) : ICreateUserCommandHandler
{
    private readonly AppDbContext _context = context;

    public async Task Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken: cancellationToken))
        {
            throw new UserNameAlreadyExistException();
        }
        User user = new()
        {
            UserName = request.UserName,
            Password = request.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}