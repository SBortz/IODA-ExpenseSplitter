using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.Interfaces;

namespace ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter;

/// <summary>
/// Currency converter core - contains business logic for currency conversion
/// </summary>
public static class CurrencyConverter_Core
{
    /// <summary>
    /// Convert currency expenses to EUR using exchange rate provider
    /// </summary>
    /// <param name="currencyExpenses">Expenses with currency information</param>
    /// <param name="exchangeRateProvider">Provider for exchange rates</param>
    /// <returns>Expenses converted to EUR</returns>
    public static EurExpense[] ConvertToEur(Expense[] currencyExpenses, ICurrencyConverter_Provider exchangeRateProvider)
    {
        var convertedExpenses = new List<EurExpense>();
        
        foreach (var currencyExpense in currencyExpenses)
        {
            double convertedAmount;
            
            if (currencyExpense.currency.ToUpper() == "EUR")
            {
                // Already in EUR, no conversion needed
                convertedAmount = currencyExpense.amount;
            }
            else
            {
                // Convert to EUR using exchange rate
                var exchangeRate = exchangeRateProvider.GetExchangeRate(currencyExpense.currency, "EUR");
                convertedAmount = currencyExpense.amount * exchangeRate.rate;
            }
            
            convertedExpenses.Add(new EurExpense(currencyExpense.name, convertedAmount));
        }
        
        return convertedExpenses.ToArray();
    }
} 