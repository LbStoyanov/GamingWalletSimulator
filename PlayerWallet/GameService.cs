
namespace PlayerWallet;


public class GameService
{
    private readonly IRandomProvider _random;

    public GameService(IRandomProvider? randomProvider = null)
    {
        _random = randomProvider ?? new RandomProvider();
    }

    public bool Play(decimal betAmount, out decimal winAmount, out string message)
    {
        winAmount = 0m;

        if (betAmount < 1 || betAmount > 10)
        {
            message = "Bet must be between $1 and $10.";
            return false;
        }

        int roll = _random.Next(1, 101);
        decimal multiplier = 0m;

        if (roll <= 50)
        {
            message = "No luck this time!";
        }
        else if (roll <= 90)
        {
            multiplier = 0.1m + (decimal)_random.NextDouble() * 1.9m; // range: [0.1, 2.0]
            winAmount = Math.Round(betAmount * multiplier, 2);
            message = $"Congrats - you won ${winAmount}!";
        }
        else
        {
            multiplier = 2.0m + (decimal)_random.NextDouble() * 8.0m; // range: [2.0, 10.0]
            winAmount = Math.Round(betAmount * multiplier, 2);
            message = $"Congrats - you won ${winAmount}!";
        }

        return true;
    }
}




