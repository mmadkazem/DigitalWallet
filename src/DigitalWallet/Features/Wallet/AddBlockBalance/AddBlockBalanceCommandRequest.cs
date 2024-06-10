namespace DigitalWallet.Features.Wallet.AddBlockBalance;


public record AddBlockBalanceCommandRequest(Guid WalletId, int BlockAmount);


