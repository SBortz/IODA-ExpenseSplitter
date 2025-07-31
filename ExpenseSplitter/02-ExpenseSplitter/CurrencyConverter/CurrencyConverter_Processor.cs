using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.Interfaces;

namespace ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter;

/// <summary>
/// Currency converter processor - orchestrates currency conversion business logic
/// </summary>
public class CurrencyConverter_Processor : ICurrencyConverter_Processor
{
    private readonly ICurrencyConverter_Provider _exchangeRateProvider;

    public CurrencyConverter_Processor(ICurrencyConverter_Provider exchangeRateProvider)
    {
        _exchangeRateProvider = exchangeRateProvider;
    }

    /// <summary>
    /// Convert currency expenses to EUR
    /// </summary>
    /// <param name="currencyExpenses">Expenses with currency information</param>
    /// <returns>Expenses converted to EUR</returns>
    public EurExpense[] ConvertToEur(CurrencyExpense[] currencyExpenses)
    {
        // Get all required exchange rates from provider (side effects happen here)
        var exchangeRates = _exchangeRateProvider.GetExchangeRatesForExpenses(currencyExpenses);
        
        // Call pure functional core with exchange rates
        return CurrencyConverter_Core.ConvertToEur(currencyExpenses, exchangeRates);
    }
} 