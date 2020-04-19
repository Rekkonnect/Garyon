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
            var nonGeneric = BenchmarkRunner.Run<ArrayCopyingHelpersVector128>();
            var generic = BenchmarkRunner.Run<ArrayCopyingHelpersGenericVector128>();
            var manual = BenchmarkRunner.Run<ArrayCopyingHelpersUnoptimized>();

            MarkdownExporter.Default.ExportToFiles(nonGeneric, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(generic, new ConsoleLogger());
            MarkdownExporter.Default.ExportToFiles(manual, new ConsoleLogger());

            SummaryExporter.ExportSummaries(4, NanoSecondsDivisor, genericUnsafe128, unsafe128, genericUnsafe256, unsafe256, manual);
        }
    }
}
