namespace DigitalWallet.Features.Wallet.MoneyTransfer;


public record MoneyTransferCommandRequest(Guid SenderWalletId, Guid ReceiptWalletId, int Amount, string Description);
public interface IMoneyTransferCommandHandler
{
    Task Handle(MoneyTransferCommandRequest request, CancellationToken cancellationToken);
}

public class MoneyTransferCommandHandler(AppDbContext context) : IMoneyTransferCommandHandler
{
    private readonly AppDbContext _context = context;

    public async Task Handle(MoneyTransferCommandRequest request, CancellationToken cancellationToken)
    {
        var senderWallet = await _context.UserWallets
            .AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == request.SenderWalletId, cancellationToken: cancellationToken)
        ?? throw new WalletNotFoundException(request.SenderWalletId);

        if (senderWallet.Balance < request.Amount)
        {
            throw new BalanceIsInsufficientException();
        }

        var receiptWallet = await _context.UserWallets
            .AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == request.ReceiptWalletId, cancellationToken: cancellationToken)
        ?? throw new WalletNotFoundException(request.ReceiptWalletId);

        if (senderWallet.Balance - request.Amount < 0)
        {
            throw new BalanceIsInsufficientException();
        }

        senderWallet.Balance -= request.Amount;
        receiptWallet.Balance += request.Amount;

        Transaction transaction = new()
        {
            Amount = request.Amount,
            Description = request.Description,
            WalletSender = senderWallet,
            WalletReceipt = receiptWallet,
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
