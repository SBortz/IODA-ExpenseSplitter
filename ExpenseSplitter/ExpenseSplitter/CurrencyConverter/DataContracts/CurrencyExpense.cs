namespace ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;

/// <summary>
/// Currency expense record - represents an expense with name, amount and currency
/// </summary>
public record CurrencyExpense(string name, double amount, string currency); 