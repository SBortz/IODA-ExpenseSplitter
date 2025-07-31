using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_ExpenseSplitter.Interfaces;

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