
namespace PlayerWallet.Tests;
public class FakeRandomProvider : IRandomProvider
{
    private readonly Queue<int> _nextValues;

    public FakeRandomProvider(params int[] values)
    {
        _nextValues = new Queue<int>(values);
    }

    public int Next(int minValue, int maxValue)
    {
        return _nextValues.Dequeue();
    }

    public double NextDouble()
    {
        return _nextValues.Dequeue() / (double)int.MaxValue;
    }
}


