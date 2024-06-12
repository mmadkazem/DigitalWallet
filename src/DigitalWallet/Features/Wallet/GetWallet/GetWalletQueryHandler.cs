namespace DigitalWallet.Features.Wallet.GetWallet;


public interface IGetWalletQueryHandler
{
    Task<GetWalletQueryResponse> Handle(GetWalletQueryRequest request, CancellationToken cancellationToken);
}

public class GetWalletQueryHandler(AppDbContext context) : IGetWalletQueryHandler
{
    private readonly AppDbContext _context = context;

    public async Task<GetWalletQueryResponse> Handle(GetWalletQueryRequest request, CancellationToken cancellationToken)
    {
        return await _context.UserWallets
                            .AsQueryable()
                            .AsNoTracking()
                            .Where(w => w.Id == request.Id)
                            .Select(w => new GetWalletQueryResponse
                            (
                                w.Id,
                                w.Balance,
                                w.BlockBalance,
                                w.CreatedOn
                            )).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new WalletNotFoundException(request.Id);
    }
}
