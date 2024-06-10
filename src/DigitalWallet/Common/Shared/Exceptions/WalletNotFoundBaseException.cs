namespace DigitalWallet.Common.Shared.Exceptions;


public abstract class WalletNotFoundBaseException(string message)
    : Exception(message) { }