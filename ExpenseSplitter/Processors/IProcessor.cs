using ExpenseSplitter.Core;
using ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.Processors;

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