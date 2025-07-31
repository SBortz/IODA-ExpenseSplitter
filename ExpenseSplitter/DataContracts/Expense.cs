namespace ExpenseSplitter.DataContracts;

/// <summary>
/// Expense record - represents an expense with name and amount
/// </summary>
public record Expense(string name, double amount); 