namespace DigitalWallet.Features.Wallet.CreateWallet;

public sealed class CreateWalletCommandValidator : AbstractValidator<CreateWalletDTO>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(r => r.Balance)
            .Must(InvalidBaseBalance).WithMessage("Volt's initial balance must be more than 50,000 tomans");
    }

    private bool InvalidBaseBalance(decimal balance)
        => balance >= 50_000;
}
