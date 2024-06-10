namespace DigitalWallet.Features.Account.Common.Exceptions;

public sealed class InValidUserNamePasswordException : WalletBadRequestBaseException
{
    public InValidUserNamePasswordException() : base("Username and password are not correct") { }
}