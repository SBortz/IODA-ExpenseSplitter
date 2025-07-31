using ExpenseSplitter.Core;

namespace ExpenseSplitter.Providers;

/// <summary>
/// Expense repository - loads expenses from file
/// </summary>
public class FileExpenseProvider : IProvider
{
    /// <summary>
    /// Load expenses from expenses.txt file
    /// </summary>
    public Expense[] Load()
    {
        return System.IO.File.ReadAllLines("expenses.txt")
            .Select(x => x.Split(','))
            .Select(x => new Expense(x[0], double.Parse(x[1])))
            .ToArray();
    }
} 