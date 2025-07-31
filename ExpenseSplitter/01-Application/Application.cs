using ExpenseSplitter._03_ExpenseSplitter.Interfaces;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._01_Application;

/// <summary>
/// Application - orchestrates the high-level flow
/// Top layer cell that coordinates middle layer cells
/// </summary>
public class Application
{
    private readonly IConsoleUI_Portal _consoleUiPortal;
    private readonly IProcessor _processor;
    private readonly IReportGenerator _reportGenerator;

    public Application(IConsoleUI_Portal consoleUiPortal, IProcessor processor, IReportGenerator reportGenerator)
    {
        _consoleUiPortal = consoleUiPortal;
        _processor = processor;
        _reportGenerator = reportGenerator;
    }

    /// <summary>
    /// Run the application - get payments, generate report, and display them
    /// </summary>
    public void Run()
    {
        try
        {
            // Process expenses and get payments
            var payments = _processor.SplitCosts();
            
            // Generate detailed report
            var report = _reportGenerator.GenerateReport(payments);
            
            // Display results
            _consoleUiPortal.Print(payments);
            
            Console.WriteLine();
            Console.WriteLine("=== DETAILED REPORT ===");
            Console.WriteLine(report);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
} 