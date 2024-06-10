namespace DigitalWallet.Features.Wallet.Common.Exceptions;


public class TransactionNotExistException : WalletNotFoundBaseException
{
    public TransactionNotExistException()
        : base("There are no transactions") { }
}