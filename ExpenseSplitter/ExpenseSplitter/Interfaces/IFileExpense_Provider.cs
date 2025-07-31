using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_ExpenseSplitter.Interfaces;

public interface IFileExpense_Provider : IProvider
{
    /// <summary>
    /// Load currency expenses from data source (with currency column)
    /// </summary>
    CurrencyExpense[] LoadExpenses();
}