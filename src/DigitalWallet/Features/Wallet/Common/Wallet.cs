namespace DigitalWallet.Features.Wallet.Common;

public class UserWallet
{
    public Guid Id { get; set; }
    public decimal Balance  { get; set; }
    public decimal BlockBalance { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;


    // User Wallet
    public User User { get; set; }
    public Guid? UserId { get; set; }
}