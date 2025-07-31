using ExpenseSplitter.Core;
using ExpenseSplitter.Providers;

namespace ExpenseSplitter.Processors;

/// <summary>
/// Processor - integrates core logic with providers
/// </summary>
public class Processor : IProcessor
{
    private readonly IProvider _repo;

    public Processor(IProvider repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Split costs by loading expenses and applying core logic
    /// </summary>
    public Payment[] SplitCosts()
    {
        var expenses = _repo.Load();
        return Splitter_Core.Split(expenses);
    }
} 