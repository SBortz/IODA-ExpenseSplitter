using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;

namespace ExpenseSplitter.Application;

/// <summary>
/// Application - orchestrates the high-level flow
/// Top layer cell that coordinates middle layer cells
/// </summary>
public class ExpenseSplitterApplication_Application
{
    private readonly IPortal _portal;
    private readonly IProcessor _processor;
    private readonly IReportGenerator _reportGenerator;

    public ExpenseSplitterApplication_Application(IPortal portal, IProcessor processor, IReportGenerator reportGenerator)
    {
        _portal = portal;
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
            _portal.Print(payments);
            
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