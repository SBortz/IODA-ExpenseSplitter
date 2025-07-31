using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter.ExpenseSplitter.Interfaces;

namespace ExpenseSplitter.ExpenseSplitter;

/// <summary>
/// Expense repository - loads expenses from file
/// </summary>
public class FileExpens_Provider : IFileExpense_Provider
{
    /// <summary>
    /// Load currency expenses from expenses.txt file (with currency column)
    /// </summary>
    public CurrencyExpense[] LoadExpenses()
    {
        return System.IO.File.ReadAllLines("expenses.txt")
            .Select(x => x.Split(','))
            .Select(x => new CurrencyExpense(x[0], double.Parse(x[1]), x[2]))
            .ToArray();
    }
} 