using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.CurrencyConverter.Interfaces;

/// <summary>
/// Currency converter provider interface for external exchange rate API
/// </summary>
public interface ICurrencyConverter_Provider
{
    /// <summary>
    /// Get exchange rate from external API
    /// </summary>
    /// <param name="fromCurrency">Source currency code</param>
    /// <param name="toCurrency">Target currency code</param>
    /// <returns>Exchange rate data</returns>
    ExchangeRate GetExchangeRate(string fromCurrency, string toCurrency);
    
    /// <summary>
    /// Get all required exchange rates for converting expenses to EUR
    /// </summary>
    /// <param name="currencyExpenses">Expenses to get exchange rates for</param>
    /// <returns>Dictionary of currency codes to EUR exchange rates</returns>
    Dictionary<string, double> GetExchangeRatesForExpenses(CurrencyExpense[] currencyExpenses);
} 