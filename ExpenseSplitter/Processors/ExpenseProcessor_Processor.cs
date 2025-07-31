using ExpenseSplitter.Core;
using ExpenseSplitter.DataContracts;
using ExpenseSplitter.Providers;

namespace ExpenseSplitter.Processors;

/// <summary>
/// Processor - integrates core logic with providers and validation
/// Middle layer cell that depends on bottom layer cells
/// </summary>
public class ExpenseProcessor_Processor : IProcessor
{
    private readonly IProvider _repo;
    private readonly ValidationEngine_Core _validator;

    public ExpenseProcessor_Processor(IProvider repo, ValidationEngine_Core validator)
    {
        _repo = repo;
        _validator = validator;
    }

    /// <summary>
    /// Split costs by loading expenses, validating them, and applying core logic
    /// </summary>
    public Payment[] SplitCosts()
    {
        var expenses = _repo.Load();
        
        // Validate expenses before processing
        var validationResult = _validator.Validate(expenses);
        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException($"Validation failed: {string.Join("; ", validationResult.Errors)}");
        }
        
        return ExpenseSplitter_Core.SplitExpenses(expenses);
    }
} 