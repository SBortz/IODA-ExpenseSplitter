using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter.ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.CurrencyConverter;

/// <summary>
/// Currency converter core - contains pure functional business logic for currency conversion
/// </summary>
public static class CurrencyConverter_Core
{
    /// <summary>
    /// Convert currency expenses to EUR using provided exchange rates
    /// Pure function - no side effects, deterministic
    /// </summary>
    /// <param name="currencyExpenses">Expenses with currency information</param>
    /// <param name="exchangeRates">Dictionary of exchange rates (currency -> EUR rate)</param>
    /// <returns>Expenses converted to EUR</returns>
    public static EurExpense[] ConvertToEur(CurrencyExpense[] currencyExpenses, Dictionary<string, double> exchangeRates)
    {
        if (currencyExpenses == null || exchangeRates == null)
            return Array.Empty<EurExpense>();
            
        return currencyExpenses
            .Select(expense => ConvertSingleExpense(expense, exchangeRates))
            .ToArray();
    }
    
    /// <summary>
    /// Convert a single expense to EUR
    /// Pure function - no side effects, deterministic
    /// </summary>
    /// <param name="expense">Expense to convert</param>
    /// <param name="exchangeRates">Dictionary of exchange rates</param>
    /// <returns>Converted expense in EUR</returns>
    private static EurExpense ConvertSingleExpense(CurrencyExpense expense, Dictionary<string, double> exchangeRates)
    {
        var currency = expense.currency.ToUpperInvariant();
        
        // If already EUR, no conversion needed
        if (currency == "EUR")
        {
            return new EurExpense(expense.name, expense.amount);
        }
        
        // Get exchange rate, default to 1.0 if not found
        var rate = exchangeRates.GetValueOrDefault(currency, 1.0);
        var convertedAmount = expense.amount * rate;
        
        return new EurExpense(expense.name, convertedAmount);
    }
} 