namespace DigitalWallet.Features.Wallet.Common;


public interface IWalletFacadeService
{
    ICreateWalletCommandHandler CreateWallet { get; }
    IMoneyTransferCommandHandler MoneyTransfer { get; }
    IAddBlockBalanceCommandHandler AddBlockBalance { get; }
    IGetTransactionsQueryHandler GetTransactions { get; }
    IGetWalletQueryHandler GetWallet { get; }
}

public class WalletFacadeService
    (ICreateWalletCommandHandler createWallet,
    AppDbContext context,
    IMoneyTransferCommandHandler moneyTransfer,
    IAddBlockBalanceCommandHandler addBlockBalance,
    IGetTransactionsQueryHandler getTransactions,
    IGetWalletQueryHandler getWallet)
: IWalletFacadeService
{
    private readonly AppDbContext _context = context;

    private readonly ICreateWalletCommandHandler _createWallet = createWallet;
    public ICreateWalletCommandHandler CreateWallet
        => _createWallet;

    private readonly IMoneyTransferCommandHandler _moneyTransfer = moneyTransfer;
    public IMoneyTransferCommandHandler MoneyTransfer
        => _moneyTransfer;

    private readonly IAddBlockBalanceCommandHandler _addBlockBalance = addBlockBalance;
    public IAddBlockBalanceCommandHandler AddBlockBalance
        => _addBlockBalance;

    private readonly IGetTransactionsQueryHandler _getTransactions = getTransactions;
    public IGetTransactionsQueryHandler GetTransactions
        => _getTransactions;

    private readonly IGetWalletQueryHandler _getWallet = getWallet;
    public IGetWalletQueryHandler GetWallet
        => _getWallet;
}