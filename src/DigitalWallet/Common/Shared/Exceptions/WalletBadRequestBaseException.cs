namespace DigitalWallet.Common.Shared.Exceptions;

public abstract class WalletBadRequestBaseException(string message)
    : Exception(message) { }