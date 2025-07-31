using ExpenseSplitter.Core;

namespace ExpenseSplitter.Portals;

/// <summary>
/// Report Generator - generates detailed reports from payments
/// Middle layer cell that depends on Core domain objects
/// </summary>
public class ReportGenerator_Portal : IReportGenerator
{
    /// <summary>
    /// Generate a detailed report from payments
    /// </summary>
    public string GenerateReport(Payment[] payments)
    {
        if (payments.Length == 0)
        {
            return "No payments to report.";
        }

        var report = new System.Text.StringBuilder();
        report.AppendLine("=== EXPENSE SPLIT REPORT ===");
        report.AppendLine($"Total participants: {payments.Length}");
        report.AppendLine();
        
        var totalPaid = payments.Where(p => p.amount < 0).Sum(p => Math.Abs(p.amount));
        var totalReceived = payments.Where(p => p.amount > 0).Sum(p => p.amount);
        
        report.AppendLine($"Total amount to be paid: {totalPaid:F2}");
        report.AppendLine($"Total amount to be received: {totalReceived:F2}");
        report.AppendLine();
        
        report.AppendLine("DETAILED BREAKDOWN:");
        foreach (var payment in payments.OrderByDescending(p => Math.Abs(p.amount)))
        {
            if (payment.amount < 0)
            {
                report.AppendLine($"  {payment.name} pays: {Math.Abs(payment.amount):F2}");
            }
            else
            {
                report.AppendLine($"  {payment.name} receives: {payment.amount:F2}");
            }
        }
        
        return report.ToString();
    }
} 