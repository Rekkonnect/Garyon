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
            var genericUnsafe128 = BenchmarkRunner.Run<ArrayCopyingHelpersGenericVector128>();
            var unsafe128 = BenchmarkRunner.Run<ArrayCopyingHelpersVector128>();
            var manual = BenchmarkRunner.Run<ArrayCopyingHelpersUnoptimized>();
            var genericUnsafe256 = BenchmarkRunner.Run<ArrayCopyingHelpersGenericVector256>();
            var unsafe256 = BenchmarkRunner.Run<ArrayCopyingHelpersVector256>();

            MarkdownExporter.Default.ExportToFiles(genericUnsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(genericUnsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(manual, new ConsoleLogger());

            SummaryExporter.ExportSummaries(4, NanoSecondsDivisor, genericUnsafe128, unsafe128, genericUnsafe256, unsafe256, manual);
        }
        public static void RunArrayBinaryOperationsHelpers()
        {
            var unsafe128 = BenchmarkRunner.Run<ArrayBinaryOperationsHelpersVector128>();
            var manual = BenchmarkRunner.Run<ArrayBinaryOperationsHelpersUnoptimized>();
            var unsafe256 = BenchmarkRunner.Run<ArrayBinaryOperationsHelpersVector256>();

            MarkdownExporter.Default.ExportToFiles(unsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(manual, new ConsoleLogger());

            SummaryExporter.ExportSummaries(2, NanoSecondsDivisor, unsafe128, unsafe256, manual);
        }
    }
}
