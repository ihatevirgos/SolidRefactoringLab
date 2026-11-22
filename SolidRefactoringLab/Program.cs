using System;

namespace SolidRefactoringLab
{

    public interface IReportFormatter
    {
        void FormatReport(string reportData);
    }

    public class PdfReportFormatter : IReportFormatter
    {
        public void FormatReport(string reportData)
        {
            Console.WriteLine("Formatting report as a PDF...");
        }
    }

    public class WordReportFormatter : IReportFormatter
    {
        public void FormatReport(string reportData)
        {
            Console.WriteLine("Formatting report as a Word Document...");
        }
    }

    public class PlainTextReportFormatter : IReportFormatter
    {
        public void FormatReport(string reportData)
        {
            Console.WriteLine("Formatting report as plain text...");
        }
    }

    // This class violates SRP and OCP
    public class ReportManager
    {
       private readonly IReportFormatter _formatter;

       public string ReportData { get; set; } = string.Empty;

       // The caller decides which formatter to pass in
       public ReportManager(IReportFormatter formatter)
       {
           _formatter = formatter;
       }

       public void GenerateReport()
       {
           // The report generation logic is now decoupled from the formatting logic
           _formatter.FormatReport(ReportData);
       }
    }

    public class ReportSaver
    {
        public void SaveToFile(string filename)
        {
            Console.WriteLine($"Saving report to {filename}...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Choose the formatter (OCP)
            IReportFormatter formatter = new PdfReportFormatter();

            // 2. Pass it to the manager
            ReportManager manager = new ReportManager(formatter);
            manager.GenerateReport();

            // 3. Use the dedicated saver (SRP)
            ReportSaver saver = new ReportSaver();
            saver.SaveToFile("Q1_Report.pdf");
        }
    }
}
