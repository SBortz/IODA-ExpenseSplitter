using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;

namespace ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.Interfaces;

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
} 