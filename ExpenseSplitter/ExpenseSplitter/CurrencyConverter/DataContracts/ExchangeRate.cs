namespace ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;

/// <summary>
/// Exchange rate record - represents exchange rate data
/// </summary>
public record ExchangeRate(string fromCurrency, string toCurrency, double rate); 