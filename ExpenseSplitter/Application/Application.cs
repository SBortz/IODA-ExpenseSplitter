using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;

namespace ExpenseSplitter.Application;

/// <summary>
/// Application - orchestrates the high-level flow
/// </summary>
public class Application
{
    private readonly IPortal _portal;
    private readonly IProcessor _processor;

    public Application(IPortal portal, IProcessor processor)
    {
        _portal = portal;
        _processor = processor;
    }

    /// <summary>
    /// Run the application - get payments and display them
    /// </summary>
    public void Run()
    {
        var payments = _processor.SplitCosts();
        _portal.Print(payments);
    }
} 