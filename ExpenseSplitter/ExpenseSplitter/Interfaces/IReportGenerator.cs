using ExpenseSplitter.BuildingBlocks;
using ExpenseSplitter.ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.Interfaces;

/// <summary>
/// Interface for generating reports from payments
/// </summary>
public interface IReportGenerator : IPortal
{
    /// <summary>
    /// Generate a detailed report from payments
    /// </summary>
    string GenerateReport(Payment[] payments);
} 