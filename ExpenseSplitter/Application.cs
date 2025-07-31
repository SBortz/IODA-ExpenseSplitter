using ExpenseSplitter.BuildingBlocks;
using ExpenseSplitter.ExpenseSplitter.Interfaces;

namespace ExpenseSplitter;

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

    public void Run()
    {
        try
        {
            var payments = _processor.SplitCosts();
            
            _consoleUiPortal.Print(payments);
            
            var report = _reportGenerator.GenerateReport(payments);
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