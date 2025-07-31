using ExpenseSplitter._02_ExpenseSplitter.DataContracts;

namespace ExpenseSplitter._99_IODA_BuildingBlocks;

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