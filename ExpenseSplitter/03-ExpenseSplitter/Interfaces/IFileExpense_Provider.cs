using ExpenseSplitter._03_ExpenseSplitter.DataContracts;

namespace ExpenseSplitter._99_IODA_BuildingBlocks;

public interface IFileExpense_Provider : IProvider
{
    /// <summary>
    /// Load expenses from data source
    /// </summary>
    Expense[] Load();
}