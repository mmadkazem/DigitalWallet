namespace DigitalWallet.Features.Wallet.CreateWallet;

public sealed class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommandRequest>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(r => r.Balance)
            .Must(InvalidBaseBalance).WithMessage("Volt's initial balance must be more than 50,000 tomans");
    }

    private bool InvalidBaseBalance(int balance)
        => balance >= 50_000;
}
