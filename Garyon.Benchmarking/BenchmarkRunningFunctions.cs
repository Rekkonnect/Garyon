namespace Garyon.Benchmarking;

public static class BenchmarkRunningFunctions
{
    public const double MinutesDivisor = 60 * SecondsDivisor;
    public const double SecondsDivisor = 1000 * MilliSecondsDivisor;
    public const double MilliSecondsDivisor = 1000 * MicroSecondsDivisor;
    public const double MicroSecondsDivisor = 1000;
    public const double NanoSecondsDivisor = 1;
}
