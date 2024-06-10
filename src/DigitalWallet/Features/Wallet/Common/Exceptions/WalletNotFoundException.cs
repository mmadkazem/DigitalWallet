namespace DigitalWallet.Features.Wallet.Common.Exceptions;


public class WalletNotFoundException(Guid Id)
    : WalletNotFoundBaseException($"There is no account with this ID: {Id}")
{
}