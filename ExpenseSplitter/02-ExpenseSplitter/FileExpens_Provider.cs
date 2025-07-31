using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.Interfaces;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;

namespace ExpenseSplitter._02_ExpenseSplitter;

/// <summary>
/// Expense repository - loads expenses from file
/// </summary>
public class FileExpens_Provider : IFileExpense_Provider
{
    /// <summary>
    /// Load currency expenses from expenses.txt file (with currency column)
    /// </summary>
    public Expense[] LoadExpenses()
    {
        return System.IO.File.ReadAllLines("expenses.txt")
            .Select(x => x.Split(','))
            .Select(x => new Expense(x[0], double.Parse(x[1]), x[2]))
            .ToArray();
    }
} 