namespace ExpenseSplitter._03_ExpenseSplitter.DataContracts;

/// <summary>
/// Result of expense validation
/// </summary>
public class ValidationResult
{
    public bool IsValid { get; set; }
    public string[] Errors { get; set; } = Array.Empty<string>();
    
    public static ValidationResult Success() => new() { IsValid = true };
    public static ValidationResult Failure(params string[] errors) => new() { IsValid = false, Errors = errors };
} 