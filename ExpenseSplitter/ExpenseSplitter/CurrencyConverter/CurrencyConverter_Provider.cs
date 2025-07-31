using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.Interfaces;

namespace ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter;

/// <summary>
/// Mock currency converter provider - simulates external exchange rate API
/// </summary>
public class CurrencyConverter_Provider : ICurrencyConverter_Provider
{
    private readonly Dictionary<string, double> _mockRates = new()
    {
        { "USD", 0.85 },  // 1 USD = 0.85 EUR
        { "GBP", 1.18 },  // 1 GBP = 1.18 EUR
        { "CHF", 0.92 },  // 1 CHF = 0.92 EUR
        { "JPY", 0.007 }, // 1 JPY = 0.007 EUR
        { "EUR", 1.0 }    // 1 EUR = 1.0 EUR
    };

    /// <summary>
    /// Get exchange rate from mock API
    /// </summary>
    /// <param name="fromCurrency">Source currency code</param>
    /// <param name="toCurrency">Target currency code</param>
    /// <returns>Exchange rate data</returns>
    public ExchangeRate GetExchangeRate(string fromCurrency, string toCurrency)
    {
        // Simulate API call delay
        Thread.Sleep(100);
        
        var fromUpper = fromCurrency.ToUpper();
        var toUpper = toCurrency.ToUpper();
        
        // If converting to EUR, use the direct rate
        if (toUpper == "EUR")
        {
            var directRate = _mockRates.GetValueOrDefault(fromUpper, 1.0);
            return new ExchangeRate(fromUpper, toUpper, directRate);
        }
        
        // For other conversions, calculate cross-rate
        var fromRate = _mockRates.GetValueOrDefault(fromUpper, 1.0);
        var toRate = _mockRates.GetValueOrDefault(toUpper, 1.0);
        var crossRate = toRate / fromRate;
        
        return new ExchangeRate(fromUpper, toUpper, crossRate);
    }
    
    /// <summary>
    /// Get all required exchange rates for converting expenses to EUR
    /// </summary>
    /// <param name="currencyExpenses">Expenses to get exchange rates for</param>
    /// <returns>Dictionary of currency codes to EUR exchange rates</returns>
    public Dictionary<string, double> GetExchangeRatesForExpenses(CurrencyExpense[] currencyExpenses)
    {
        var exchangeRates = new Dictionary<string, double>();
        
        // Extract unique currencies from expenses
        var uniqueCurrencies = currencyExpenses
            .Select(e => e.currency.ToUpperInvariant())
            .Distinct();
        
        foreach (var currency in uniqueCurrencies)
        {
            if (currency != "EUR") // EUR doesn't need conversion
            {
                var rate = GetExchangeRate(currency, "EUR");
                exchangeRates[currency] = rate.rate;
            }
        }
        
        return exchangeRates;
    }
} 