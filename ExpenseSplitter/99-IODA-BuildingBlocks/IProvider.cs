using ExpenseSplitter._03_ExpenseSplitter.DataContracts;

namespace ExpenseSplitter._99_IODA_BuildingBlocks;

/// <summary>
/// Provider interface for loading expenses
/// </summary>
public interface IProvider
{
    /// <summary>
    /// Load expenses from data source
    /// </summary>
    Expense[] Load();
} 