namespace DigitalWallet.Features.Wallet.Common.Exceptions;


public class UserNotFoundException()
    : WalletNotFoundBaseException("No user found with this information") { }