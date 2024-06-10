namespace DigitalWallet.Features.Wallet.AddBlockBalance;
public interface IAddBlockBalanceCommandHandler
{
    Task Handle(AddBlockBalanceCommandRequest request, CancellationToken cancellationToken);
}
public class AddBlockBalanceCommandHandler(AppDbContext context) : IAddBlockBalanceCommandHandler
{
    private readonly AppDbContext _context = context;

    public async Task Handle(AddBlockBalanceCommandRequest request, CancellationToken cancellationToken)
    {
        var wallet = await _context.UserWallets
            .AsQueryable()
            .FirstOrDefaultAsync(w => w.Id == request.WalletId, cancellationToken)
        ?? throw new WalletNotFoundException(request.WalletId);

        if (wallet.Balance < request.BlockAmount)
        {
            throw new BalanceIsInsufficientException();
        }

        wallet.Balance -= request.BlockAmount;
        wallet.BlockBalance = request.BlockAmount;
        _context.UserWallets.Update(wallet);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
