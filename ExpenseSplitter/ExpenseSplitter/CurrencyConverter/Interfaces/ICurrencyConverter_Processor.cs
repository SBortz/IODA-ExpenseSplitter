using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter.ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.CurrencyConverter.Interfaces;

/// <summary>
/// Currency converter processor interface for business logic orchestration
/// </summary>
public interface ICurrencyConverter_Processor
{
    /// <summary>
    /// Convert currency expenses to EUR
    /// </summary>
    /// <param name="currencyExpenses">Expenses with currency information</param>
    /// <returns>Expenses converted to EUR</returns>
    EurExpense[] ConvertToEur(CurrencyExpense[] currencyExpenses);
} 