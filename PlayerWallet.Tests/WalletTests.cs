using Xunit;

namespace PlayerWallet.Tests;
public class WalletTests
{

    private readonly Wallet _wallet;

    public WalletTests()
    {
        _wallet = new Wallet();
    }

    //Deposit Method Tests

    [Fact]
    public void Deposit_Should_Increase_Balance()
    {

        _wallet.Deposit(50, out _);

        Assert.Equal(50, _wallet.Balance);
    }


    [Fact]
    public void Deposit_Zero_Amount_Should_Fail()
    {
       
        var result = _wallet.Deposit(0, out string message);

        Assert.False(result);
        Assert.Contains("positive", message);
        Assert.Equal(0, _wallet.Balance);
    }

    [Fact]
    public void Deposit_Negative_Amount_Should_Fail()
    {
      
        var result = _wallet.Deposit(-10, out string message);

        Assert.False(result);
        Assert.Contains("positive", message);
        Assert.Equal(0, _wallet.Balance);
    }

    [Fact]
    public void Deposit_Positive_Amount_Should_Succeed_With_Message()
    {
        
        var result = _wallet.Deposit(25, out string message);

        Assert.True(result);
        Assert.Contains("successful", message);
        Assert.Contains("25", message);
        Assert.Equal(25, _wallet.Balance);
    }

    [Fact]
    public void Multiple_Deposits_Should_Accumulate_Balance()
    {

        _wallet.Deposit(10, out _);
        _wallet.Deposit(20, out _);

        Assert.Equal(30, _wallet.Balance);
    }


    //Withdraw Method Tests

    [Fact]
    public void Withdraw_More_Than_Balance_Should_Fail()
    {

        _wallet.Deposit(20, out _);
        var result = _wallet.Withdraw(50, out string message);

        Assert.False(result);
        Assert.Contains("Insufficient", message);
        Assert.Equal(20, _wallet.Balance);
    }

    [Fact]
    public void Withdraw_Should_Decrease_Balance()
    {

        _wallet.Deposit(100, out _);
        _wallet.Withdraw(40, out _);

        Assert.Equal(60, _wallet.Balance);
    }

    [Fact]
    public void Withdraw_Negative_Amount_Should_Fail()
    {
        
        _wallet.Deposit(100, out _); 
        var result = _wallet.Withdraw(-10, out string message);

       
        Assert.False(result);
        Assert.Contains("positive", message);  
        Assert.Equal(100, _wallet.Balance); 
    }

    [Fact]
    public void Withdraw_Zero_Amount_Should_Fail()
    {
        
        _wallet.Deposit(100, out _); 
        var result = _wallet.Withdraw(0, out string message);

      
        Assert.False(result);
        Assert.Contains("positive", message); 
        Assert.Equal(100, _wallet.Balance);  
    }

    [Fact]
    public void Withdraw_Amount_Exceeding_Balance_Should_Fail()
    {
   
        _wallet.Deposit(50, out _);  
        var result = _wallet.Withdraw(100, out string message); 

  
        Assert.False(result);
        Assert.Contains("Insufficient", message); 
        Assert.Equal(50, _wallet.Balance); 
    }

    [Fact]
    public void Withdraw_Valid_Amount_Should_Succeed()
    {
     
        _wallet.Deposit(100, out _); 
        var result = _wallet.Withdraw(40, out string message); 

        Assert.True(result);
        Assert.Contains("successful", message);  
        Assert.Contains("40", message);  
        Assert.Equal(60, _wallet.Balance);  
    }


    //ApplyGameResult Method Tests

    [Fact]
    public void ApplyGameResult_Should_Update_Balance_After_Win()
    {
       
        _wallet.Deposit(100, out _);  
        decimal bet = 30;
        decimal win = 60; 

        
        _wallet.ApplyGameResult(bet, win);

       
        Assert.Equal(130, _wallet.Balance);  // Balance should be 100 - 30 + 60 = 130
    }

    [Fact]
    public void ApplyGameResult_Should_Update_Balance_After_Loss()
    {
        
        _wallet.Deposit(100, out _);  
        decimal bet = 40;
        decimal win = 0;  

        
        _wallet.ApplyGameResult(bet, win);

      
        Assert.Equal(60, _wallet.Balance);  
    }

    [Fact]
    public void ApplyGameResult_Should_Not_Change_Balance_When_Bet_Is_Zero()
    {
        
        _wallet.Deposit(100, out _);  
        decimal bet = 0;
        decimal win = 0;  

        
        _wallet.ApplyGameResult(bet, win);

       
        Assert.Equal(100, _wallet.Balance);  
    }

    [Fact]
    public void ApplyGameResult_Should_Update_Balance_When_Bet_And_Win_Are_Valid()
    {

        decimal initialBalance = _wallet.Balance;
        decimal bet = 5m;
        decimal win = 10m;

        _wallet.ApplyGameResult(bet, win);

        Assert.Equal(initialBalance - bet + win, _wallet.Balance);
    }

    [Fact]
    public void ApplyGameResult_Should_Not_Update_Balance_When_Bet_Is_Negative()
    {
       
        decimal initialBalance = _wallet.Balance;
        decimal bet = -5m;
        decimal win = 10m;

       
        _wallet.ApplyGameResult(bet, win);

        
        Assert.Equal(initialBalance, _wallet.Balance);  
    }

    [Fact]
    public void ApplyGameResult_Should_Not_Update_Balance_When_Win_Is_Negative()
    {
        
        decimal initialBalance = _wallet.Balance;
        decimal bet = 5m;
        decimal win = -10m;

        
        _wallet.ApplyGameResult(bet, win);

        
        Assert.Equal(initialBalance, _wallet.Balance);  
    }

    [Fact]
    public void ApplyGameResult_Should_Not_Update_Balance_When_Bet_And_Win_Are_Negative()
    {
        
        decimal initialBalance = _wallet.Balance;
        decimal bet = -5m;
        decimal win = -10m;

       
        _wallet.ApplyGameResult(bet, win);

        
        Assert.Equal(initialBalance, _wallet.Balance);  
    }
}

