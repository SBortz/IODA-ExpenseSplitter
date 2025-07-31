using ExpenseSplitter.ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.BuildingBlocks;

/// <summary>
/// Processor interface for business logic orchestration
/// </summary>
public interface IProcessor
{
    /// <summary>
    /// Split costs and return payments
    /// </summary>
    Payment[] SplitCosts();
} 