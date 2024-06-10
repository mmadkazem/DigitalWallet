namespace DigitalWallet.Features.Account.Common;


public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public UserWallet UserWallet { get; set; }
    public Guid? UserWalletId { get; set; }
}