using ExpenseSplitter.Core;

namespace ExpenseSplitter.Providers;

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