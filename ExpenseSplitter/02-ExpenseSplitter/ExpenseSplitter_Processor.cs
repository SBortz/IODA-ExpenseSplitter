using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.Interfaces;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter.Interfaces;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_ExpenseSplitter;

public class ExpenseSplitter_Processor : IProcessor
{
    private readonly IFileExpense_Provider _repo;
    private readonly ICurrencyConverter_Processor _currencyConverter;

    public ExpenseSplitter_Processor(IFileExpense_Provider repo, ICurrencyConverter_Processor currencyConverter)
    {
        _repo = repo;
        _currencyConverter = currencyConverter;
    }

    public Payment[] SplitCosts()
    {
        // Load currency expenses and convert to EUR
        Expense[] expenses = _repo.LoadExpenses();
        EurExpense[] expensesEuro = _currencyConverter.ConvertToEur(expenses);
        
        return ExpenseSplitter_Core.SplitExpenses(expensesEuro);
    }
} 