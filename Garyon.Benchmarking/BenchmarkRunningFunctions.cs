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
            var genericUnsafe128 = BenchmarkRunner.Run<ArrayCopyingHelpersGenericVector128>();
            var genericUnsafe256 = BenchmarkRunner.Run<ArrayCopyingHelpersGenericVector256>();
            var unvirtualized128 = BenchmarkRunner.Run<ArrayCopyingHelpersUnvirtualizedVector128>();
            var unvirtualized256 = BenchmarkRunner.Run<ArrayCopyingHelpersUnvirtualizedVector256>();
            var unoptimized = BenchmarkRunner.Run<ArrayCopyingHelpersUnoptimized>();

            MarkdownExporter.Default.ExportToFiles(genericUnsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unvirtualized128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(genericUnsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unsafe256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unvirtualized256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unoptimized, new ConsoleLogger());

            SummaryExporter.ExportSummaries(6, NanoSecondsDivisor, genericUnsafe128, unsafe128, unvirtualized128, genericUnsafe256, unsafe256, unvirtualized256, unoptimized);
        }
        public static void RunUnvirtualizedArrayCopyingHelpers()
        {
            var unvirtualized128 = BenchmarkRunner.Run<ArrayCopyingHelpersUnvirtualizedVector128>();
            var unvirtualized256 = BenchmarkRunner.Run<ArrayCopyingHelpersUnvirtualizedVector256>();
            var unoptimized = BenchmarkRunner.Run<ArrayCopyingHelpersUnoptimized>();

            MarkdownExporter.Default.ExportToFiles(unvirtualized128, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unvirtualized256, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(unoptimized, new ConsoleLogger());

            SummaryExporter.ExportSummaries(2, NanoSecondsDivisor, unvirtualized128, unvirtualized256, unoptimized);
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
