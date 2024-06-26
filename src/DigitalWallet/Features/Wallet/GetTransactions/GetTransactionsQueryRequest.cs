namespace DigitalWallet.Features.Wallet.GetTransactions;


public record GetTransactionsQueryRequest(Guid WalletId);

public class GetTransactionsQueryHandler(AppDbContext context) : IGetTransactionsQueryHandler
{
    private readonly AppDbContext _context = context;

    public async Task<List<GetTransactionsQueryResponse>> Handle(GetTransactionsQueryRequest request, CancellationToken cancellationToken)
    {
        var responses = await _context.Transactions
                            .AsQueryable()
                            .AsNoTracking()
                            .Where(t => t.WalletReceipt.Id == request.WalletId || t.WalletSender.Id == request.WalletId)
                            .Select(t => new GetTransactionsQueryResponse
                            (
                                t.Id,
                                t.Amount,
                                t.Description,
                                t.PayDateOn,
                                t.WalletSender.Id,
                                t.WalletReceipt.Id
                            )).ToListAsync(cancellationToken);
        if (responses.Count == 0)
        {
            throw new TransactionNotExistException();
        }

        return responses;
    }
}

public interface IGetTransactionsQueryHandler
{
    Task<List<GetTransactionsQueryResponse>> Handle(GetTransactionsQueryRequest request, CancellationToken cancellationToken);
}

public record GetTransactionsQueryResponse
(
    Guid Id,
    int Amount,
    string Description,
    DateTime PayDateOn,
    Guid WalletSenderId,
    Guid WalletReceiptId
);