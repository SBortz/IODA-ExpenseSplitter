using ExpenseSplitter.Core;

namespace ExpenseSplitter.Portals;

/// <summary>
/// Interface for generating reports from payments
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Generate a detailed report from payments
    /// </summary>
    string GenerateReport(Payment[] payments);
} 