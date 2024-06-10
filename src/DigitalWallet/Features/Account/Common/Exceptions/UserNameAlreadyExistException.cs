namespace DigitalWallet.Features.Account.Common.Exceptions;


public class UserNameAlreadyExistException()
    : WalletBadRequestBaseException("This user name already exists") { }