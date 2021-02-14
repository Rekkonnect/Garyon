using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Garyon.Benchmarking.Exporting;
using Garyon.Benchmarking.Extensions;

namespace Garyon.Benchmarking
{
    public static class BenchmarkRunningFunctions
    {
        public const double MinutesDivisor = 60 * SecondsDivisor;
        public const double SecondsDivisor = 1000 * MilliSecondsDivisor;
        public const double MilliSecondsDivisor = 1000 * MicroSecondsDivisor;
        public const double MicroSecondsDivisor = 1000;
        public const double NanoSecondsDivisor = 1;

        public static void RunArrayCopyingHelpers()
        {
            var unsafe128 = BenchmarkRunner.Run<ArrayCopyingHelpersVector128>();
            var unsafe256 = BenchmarkRunner.Run<ArrayCopyingHelpersVector256>();
            var unoptimized = BenchmarkRunner.Run<ArrayCopyingHelpersUnoptimized>();

            MarkdownExporter.Default.ExportToFiles(unsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unoptimized, new ConsoleLogger());

            SummaryExporter.ExportSummaries(2, NanoSecondsDivisor, unsafe128, unsafe256, unoptimized);
        }
        public static void RunArrayBitwiseOperationsHelpers()
        {
            var unsafe128 = BenchmarkRunner.Run<ArrayBitwiseOperationsHelpersVector128>();
            var unsafe256 = BenchmarkRunner.Run<ArrayBitwiseOperationsHelpersVector256>();
            var unoptimized = BenchmarkRunner.Run<ArrayBitwiseOperationsHelpersUnoptimized>();

            MarkdownExporter.Default.ExportToFiles(unsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unoptimized, new ConsoleLogger());

            SummaryExporter.ExportSummaries(2, NanoSecondsDivisor, unsafe128, unsafe256, unoptimized);
        }
    }
}
