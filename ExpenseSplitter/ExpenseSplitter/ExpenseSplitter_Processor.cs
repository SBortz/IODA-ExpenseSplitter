using ExpenseSplitter.BuildingBlocks;
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.Interfaces;
using ExpenseSplitter.ExpenseSplitter.DataContracts;
using ExpenseSplitter.ExpenseSplitter.Interfaces;

namespace ExpenseSplitter.ExpenseSplitter;

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
        var currencyExpenses = _repo.LoadExpenses();
        var expensesEuro = _currencyConverter.ConvertToEur(currencyExpenses);
        
        return ExpenseSplitter_Core.SplitExpenses(expensesEuro);
    }
} 