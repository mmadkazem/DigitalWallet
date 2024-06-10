namespace DigitalWallet.Features.Wallet.CreateWallet;


public record CreateWalletCommandRequest(Guid UserId, int Balance);

public interface ICreateWalletCommandHandler
{
    Task Handle(CreateWalletCommandRequest request, CancellationToken cancellationToken);
}
public class CreateWalletCommandHandler(AppDbContext context) : ICreateWalletCommandHandler
{
    private readonly AppDbContext _context = context;

    public async Task Handle(CreateWalletCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _context.UserWallets.AnyAsync(u => u.UserId == request.UserId, cancellationToken))
        {
            throw new WalletAlreadyExistException();
        }

        var user = await _context.Users.AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
        ?? throw new UserNotFoundException();

        UserWallet userWallet = new()
        {
            Balance = request.Balance,
            User = user,
            UserId = user.Id
        };

        _context.UserWallets.Add(userWallet);
        await _context.SaveChangesAsync(cancellationToken);
    }
}