using ExpenseSplitter._03_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._03_ExpenseSplitter;

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