
using Xunit;

namespace PlayerWallet.Tests;

public class GameServiceTests
{
    // Test for invalid bets
    [Fact]
    public void Play_BetIsTooLow_ReturnsErrorMessage()
    {
     
        var fakeRandomProvider = new FakeRandomProvider(1);
        var gameService = new GameService(fakeRandomProvider);

       
        bool result = gameService.Play(0.5m, out decimal winAmount, out string message);

        
        Assert.False(result);
        Assert.Equal("Bet must be between $1 and $10.", message);
    }

    [Fact]
    public void Play_BetIsTooHigh_ReturnsErrorMessage()
    {
       
        var fakeRandomProvider = new FakeRandomProvider(1);
        var gameService = new GameService(fakeRandomProvider);

        
        bool result = gameService.Play(15m, out decimal winAmount, out string message);

       
        Assert.False(result);
        Assert.Equal("Bet must be between $1 and $10.", message);
    }

    // Test for valid bets
    [Fact]
    public void Play_BetIsValidAndLose_ReturnsLoseMessage()
    {
        
        var fakeRandomProvider = new FakeRandomProvider(1); // Roll <= 50
        var gameService = new GameService(fakeRandomProvider);

       
        bool result = gameService.Play(5m, out decimal winAmount, out string message);

        Assert.True(result);
        Assert.Equal("No luck this time!", message);
        Assert.Equal(0m, winAmount);
    }

    [Fact]
    public void Play_BetIsValidAndWinLowMultiplier_ReturnsWinMessage()
    {
       
        var fakeRandomProvider = new FakeRandomProvider(60, 500); // Roll > 50 and random multiplier
        var gameService = new GameService(fakeRandomProvider);

      
        bool result = gameService.Play(5m, out decimal winAmount, out string message);

        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount > 0m && winAmount <= 10m);
    }

    [Fact]
    public void Play_BetIsValidAndWinHighMultiplier_ReturnsWinMessage()
    {
        
        var fakeRandomProvider = new FakeRandomProvider(95, 1000); // Roll > 90 and random multiplier in high range
        var gameService = new GameService(fakeRandomProvider);

       
        bool result = gameService.Play(5m, out decimal winAmount, out string message);

      
        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount >= 10m && winAmount <= 50m);
    }


    // Randomness Edge Case Tests
    [Fact]
    public void Play_BetWithRandomnessWithinValidRange_ReturnsCorrectMultiplier()
    {
       
        var fakeRandomProvider = new FakeRandomProvider(60, 500); // Simulate a win with valid multiplier range
        var gameService = new GameService(fakeRandomProvider);

       
        bool result = gameService.Play(5m, out decimal winAmount, out string message);

   
        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount >= 0.5m && winAmount <= 10m); // 5m bet * multiplier [0.1, 2.0]
    }

    [Fact]
    public void Play_BetWithHighRandomnessMultiplier_ReturnsCorrectMultiplier()
    {
        
        var fakeRandomProvider = new FakeRandomProvider(95, 500); // Simulate a win with high multiplier range
        var gameService = new GameService(fakeRandomProvider);

    
        bool result = gameService.Play(5m, out decimal winAmount, out string message);

        
        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount >= 10m && winAmount <= 50m); // 5m bet * multiplier [2.0, 10.0]
    }

   
    // Boundary Tests for Bet Amount
    [Fact]
    public void Play_BetIsExactlyOne_ReturnsValidResult()
    {
        
        var fakeRandomProvider = new FakeRandomProvider(60, 500); // Valid roll > 50 and a random multiplier
        var gameService = new GameService(fakeRandomProvider);

        
        bool result = gameService.Play(1m, out decimal winAmount, out string message);

       
        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount > 0m && winAmount <= 2m); // Expected multiplier should be within range
    }

    [Fact]
    public void Play_BetIsExactlyTen_ReturnsValidResult()
    {
       
        var fakeRandomProvider = new FakeRandomProvider(60, 500); // Valid roll > 50 and random multiplier
        var gameService = new GameService(fakeRandomProvider);

       
        bool result = gameService.Play(10m, out decimal winAmount, out string message);

      
        Assert.True(result);
        Assert.StartsWith("Congrats - you won $", message);
        Assert.True(winAmount > 0m && winAmount <= 20m); // Expected multiplier should be within range
    }
}

