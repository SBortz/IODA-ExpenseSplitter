using ExpenseSplitter.BuildingBlocks;
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.Interfaces;

public interface IFileExpense_Provider : IProvider
{
    /// <summary>
    /// Load currency expenses from data source (with currency column)
    /// </summary>
    CurrencyExpense[] LoadExpenses();
}