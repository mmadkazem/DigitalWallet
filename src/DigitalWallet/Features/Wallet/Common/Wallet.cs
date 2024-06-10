namespace DigitalWallet.Features.Wallet.Common;

public class UserWallet
{
    public Guid Id { get; set; }
    public int Balance  { get; set; }
    public int BlockBalance { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;


    // User Wallet
    public User User { get; set; }
    public Guid? UserId { get; set; }
}