
namespace PlayerWallet;

internal class Program
{
    static void Main(string[] args)
    {
        var wallet = new Wallet();
        var game = new GameService();

        while (true)
        {
            Console.WriteLine("Please, submit action:");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(input)) continue;

            if (input == "exit")
            {
                Console.WriteLine("Thank you for playing! Hope to see you again soon.");
                break;
            }

            try
            {
                
                var userInput = input.Split(' ');
                var command = userInput[0];

                if (userInput.Length < 2 || !decimal.TryParse(userInput[1], out decimal amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid command and amount.");
                    continue;
                }

                switch (command)
                {
                    case "deposit":
                        wallet.Deposit(amount, out string depositMessage);
                        Console.WriteLine(depositMessage);
                        break;

                    case "withdraw":
                        wallet.Withdraw(amount, out string withdrawMessage);
                        Console.WriteLine(withdrawMessage);
                        break;

                    case "bet":
                        if (amount > wallet.Balance)
                        {
                            Console.WriteLine($"Insufficient balance. Your current balance is: ${wallet.Balance}");
                            break;
                        }

                        if (game.Play(amount, out decimal win, out string gameMessage))
                        {
                            wallet.ApplyGameResult(amount, win);
                            Console.WriteLine($"{gameMessage} Your current balance is: ${wallet.Balance}");
                        }
                        else
                        {
                            Console.WriteLine(gameMessage);
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Please try again.");
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

