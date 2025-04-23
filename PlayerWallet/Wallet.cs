
namespace PlayerWallet;

public class Wallet
{
    public decimal Balance { get; private set; }

    public Wallet()
    {
        Balance = 0m;
    }

    public bool Deposit(decimal amount, out string message)
    {
        if (amount <= 0)
        {
            message = "Deposit must be a positive amount.";
            return false;
        }

        Balance += amount;
        message = $"Your deposit of ${amount} was successful. Your current balance is: ${Balance}";
        return true;
    }

    public bool Withdraw(decimal amount, out string message)
    {
        if (amount <= 0)
        {
            message = "Withdrawal must be a positive amount.";
            return false;
        }

        if (amount > Balance)
        {
            message = $"Insufficient balance. Your current balance is: ${Balance}";
            return false;
        }

        Balance -= amount;
        message = $"Your withdrawal of ${amount} was successful. Your current balance is: ${Balance}";
        return true;
    }

    public void ApplyGameResult(decimal bet, decimal win)
    {
        if (bet < 0 || win < 0) return;

        Balance -= bet;
        Balance += win;
    }
}

