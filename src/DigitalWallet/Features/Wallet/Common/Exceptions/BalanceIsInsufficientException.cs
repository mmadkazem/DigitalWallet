namespace DigitalWallet.Features.Wallet.Common.Exceptions;

public class BalanceIsInsufficientException()
    : WalletBadRequestBaseException("The wallet balance is insufficient")
{
}