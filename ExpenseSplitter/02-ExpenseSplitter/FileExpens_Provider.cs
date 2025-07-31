using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.Interfaces;

namespace ExpenseSplitter._02_ExpenseSplitter;

/// <summary>
/// Expense repository - loads expenses from file
/// </summary>
public class FileExpens_Provider : IFileExpense_Provider
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