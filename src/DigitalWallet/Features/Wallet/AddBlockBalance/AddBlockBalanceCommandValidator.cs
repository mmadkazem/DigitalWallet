namespace DigitalWallet.Features.Wallet.AddBlockBalance;

public class AddBlockBalanceCommandValidator : AbstractValidator<AddBlockBalanceCommandRequest>
{
    public AddBlockBalanceCommandValidator()
    {
        RuleFor(r => r.BlockAmount)
            .Must(InvalidBaseBalance).WithMessage("Volt's initial balance must be more than 50,000 tomans");

    }
    private bool InvalidBaseBalance(int balance)
        => balance > 0;
}