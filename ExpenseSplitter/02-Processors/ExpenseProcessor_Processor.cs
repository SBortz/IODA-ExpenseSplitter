using ExpenseSplitter._03_ExpenseSplitter;
using ExpenseSplitter._03_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_Processors;

/// <summary>
/// Processor - integrates core logic with providers and validation
/// Middle layer cell that depends on bottom layer cells
/// </summary>
public class ExpenseProcessor_Processor : IProcessor
{
    private readonly IProvider _repo;

    public ExpenseProcessor_Processor(IProvider repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Split costs by loading expenses, validating them, and applying core logic
    /// </summary>
    public Payment[] SplitCosts()
    {
        var expenses = _repo.Load();
        return ExpenseSplitter_Core.SplitExpenses(expenses);
    }
} 