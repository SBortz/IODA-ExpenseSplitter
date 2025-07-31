using ExpenseSplitter.Core;

namespace ExpenseSplitter.Portals;

/// <summary>
/// Portal interface for user interaction and output
/// </summary>
public interface IPortal
{
    /// <summary>
    /// Print payments to user
    /// </summary>
    void Print(Payment[] payments);
} 