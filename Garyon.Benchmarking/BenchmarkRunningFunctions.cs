using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Garyon.Benchmarking.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

            ExportSummaries(2, NanoSecondsDivisor, nonGeneric, generic, manual);
        }

        public static void ExportSummaries(int baselineIndex, double nanosecondsDivisor, params Summary[] summaries)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Overview");

                var cells = worksheet.Cells;

                for (int i = 1; i <= 2; i++)
                {
                    var row = worksheet.Row(i);
                    row.Style.Font.Bold = true;
                    row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                cells["A1"].Value = "Method";
                cells["A1:A2"].Merge = true;

                var benchNames = new Dictionary<string, int>();
                int baselineColumn = 2 + baselineIndex * 4;

                for (int i = 0; i < summaries.Length; i++)
                {
                    int baseColumn = 2 + i * 4;
                    cells[1, baseColumn, 1, baseColumn + 3].Merge = true;
                    cells[1, baseColumn].Value = summaries[i].BenchmarksCases[0].Descriptor.Categories[1];
                    cells[2, baseColumn].Value = "Mean";
                    cells[2, baseColumn + 1].Value = "Error";
                    cells[2, baseColumn + 2].Value = "StdDev";
                    cells[2, baseColumn + 3].Value = "Ratio";

                    foreach (var r in summaries[i].Reports)
                    {
                        var b = r.BenchmarkCase;
                        var category = b.Descriptor.Categories[0];

                        if (!benchNames.TryGetValue(category, out int index))
                        {
                            benchNames.Add(category, index = benchNames.Count);
                            cells[3 + index, 1].Value = category;
                        }

                        var mean = r.ResultStatistics.Mean / nanosecondsDivisor;
                        var error = r.ResultStatistics.StandardError / nanosecondsDivisor;
                        var stdDev = r.ResultStatistics.StandardDeviation / nanosecondsDivisor;

                        var meanAddress = ExcelCellBase.GetAddress(3 + index, baseColumn);
                        var baselineMeanAddress = ExcelCellBase.GetAddress(3 + index, baselineColumn);

                        cells[meanAddress].Value = mean;
                        cells[3 + index, baseColumn + 1].Value = error;
                        cells[3 + index, baseColumn + 2].Value = stdDev;
                        cells[3 + index, baseColumn + 3].Formula = $"{meanAddress}/{baselineMeanAddress}";
                    }
                }

                worksheet.Column(1).AutoFit();

                int lastRow = 2 + benchNames.Count;

                for (int i = 0; i < summaries.Length; i++)
                {
                    int baseColumn = 2 + i * 4;

                    // Using this is not working because upon retrieving the range the result is probably a singleton :)
                    //var meansRange = cells[3, baseColumn, lastRow, baseColumn];
                    //var timeValuesRange = cells[3, baseColumn, lastRow, baseColumn + 2];
                    //var ratiosRange = cells[3, baseColumn + 3, lastRow, baseColumn + 3];

                    if (i == baselineIndex)
                        AddDatabar(0, 176, 240);
                    else
                    {
                        var ratioRule = cells[3, baseColumn + 3, lastRow, baseColumn + 3].ConditionalFormatting.AddGreaterThan();
                        ratioRule.Formula = "1";
                        ratioRule.Style.Fill.BackgroundColor.Color = Color.FromArgb(255, 199, 206);
                        ratioRule.Style.Font.Color.Color = Color.FromArgb(156, 0, 0);

                        AddDatabar(0, 218, 100);
                    }

                    cells[3, baseColumn, lastRow, baseColumn + 2].Style.Numberformat.Format = "#,##0.00";
                    cells[3, baseColumn + 3, lastRow, baseColumn + 3].Style.Numberformat.Format = "0.0000";

                    void AddDatabar(int r, int g, int b)
                    {
                        var bar = cells[3, baseColumn, lastRow, baseColumn].ConditionalFormatting.AddDatabar(Color.FromArgb(r, g, b));
                        // Hope that this indicates automatic
                        bar.HighValue.Type = (eExcelConditionalFormattingValueObjectType)6;
                        bar.LowValue.Type = (eExcelConditionalFormattingValueObjectType)6;
                        bar.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        // cannot set the data bar to a solid color? :(?
                    }
                }

                // Set some document properties
                package.Workbook.Properties.Title = "Benchmark Results";

                package.SaveAs(new FileInfo("Benchmark Results.xlsx"));
            }
        }
    }
}
