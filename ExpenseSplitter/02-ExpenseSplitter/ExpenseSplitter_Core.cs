using ExpenseSplitter._02_ExpenseSplitter.DataContracts;

namespace ExpenseSplitter._02_ExpenseSplitter;

/// <summary>
/// Core business logic for splitting expenses
/// Contains pure domain logic without any external dependencies
/// </summary>
public static class ExpenseSplitter_Core
{
    /// <summary>
    /// Split expenses among people and calculate who pays/receives what
    /// </summary>
    public static Payment[] SplitExpenses(Expense[] expenses)
    {
        // Handle empty or single expense arrays
        if (expenses.Length <= 1)
        {
            return new Payment[0];
        }

        // Calculate average of all expenses
        var av = expenses.Select(x => x.amount).Average();
        
        // Filter expenses that are not equal to average and create payments
        return expenses
            .Where(x => x.amount != av)
            .Select(x => new Payment(x.name, x.amount - av))
            .ToArray();
    }
} 