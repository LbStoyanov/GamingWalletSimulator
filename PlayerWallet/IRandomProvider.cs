

namespace PlayerWallet;
public interface IRandomProvider
{
    int Next(int minValue, int maxValue);          // For roll (1–100)
    double NextDouble();                           // For multiplier
}

