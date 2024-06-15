namespace DigitalWallet.Features.Wallet.Common;

public class Transaction
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = null!;
    public DateTime PayDateOn { get; set; } = DateTime.Now;

    // Wallet Transaction
    public UserWallet WalletSender { get; set; }
    // public Guid WalletSenderId { get; set; }

    public UserWallet WalletReceipt { get; set; }
    // public Guid WalletReceiptId { get; set; }
}