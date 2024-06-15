using Microsoft.AspNetCore.Server.HttpSys;

namespace DigitalWallet.Features.Wallet.GetWallet;


public record GetWalletQueryRequest(Guid Id);
public record GetWalletQueryResponse(Guid Id, decimal Balance, decimal BlockBalance, DateTime CreatedOn);

