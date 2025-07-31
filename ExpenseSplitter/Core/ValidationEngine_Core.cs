using ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.Core;

/// <summary>
/// Validation Engine - validates expenses before processing
/// Bottom layer cell that provides validation services
/// </summary>
public class ValidationEngine_Core
{
    /// <summary>
    /// Validate expenses and return validation result
    /// </summary>
    public ValidationResult Validate(Expense[] expenses)
    {
        var errors = new List<string>();
        
        if (expenses == null || expenses.Length == 0)
        {
            errors.Add("No expenses provided");
            return ValidationResult.Failure(errors.ToArray());
        }
        
        if (expenses.Length == 1)
        {
            errors.Add("At least 2 expenses are required for splitting");
            return ValidationResult.Failure(errors.ToArray());
        }
        
        // Check for duplicate names
        var duplicateNames = expenses
            .GroupBy(e => e.name)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
            
        if (duplicateNames.Any())
        {
            errors.Add($"Duplicate names found: {string.Join(", ", duplicateNames)}");
        }
        
        // Check for negative amounts
        var negativeAmounts = expenses.Where(e => e.amount < 0).ToArray();
        if (negativeAmounts.Any())
        {
            errors.Add($"Negative amounts not allowed for: {string.Join(", ", negativeAmounts.Select(e => e.name))}");
        }
        
        // Check for zero amounts
        var zeroAmounts = expenses.Where(e => e.amount == 0).ToArray();
        if (zeroAmounts.Any())
        {
            errors.Add($"Zero amounts not allowed for: {string.Join(", ", zeroAmounts.Select(e => e.name))}");
        }
        
        return errors.Count > 0 
            ? ValidationResult.Failure(errors.ToArray())
            : ValidationResult.Success();
    }
} 