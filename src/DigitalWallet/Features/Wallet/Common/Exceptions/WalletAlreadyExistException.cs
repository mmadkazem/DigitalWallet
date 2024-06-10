namespace DigitalWallet.Features.Wallet.Common.Exceptions;


public class WalletAlreadyExistException : WalletBadRequestBaseException
{
    public WalletAlreadyExistException() : base("This user already has a wallet") { }
}