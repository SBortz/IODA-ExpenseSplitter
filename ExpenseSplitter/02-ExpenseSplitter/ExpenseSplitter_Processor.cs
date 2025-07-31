using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._02_ExpenseSplitter.Interfaces;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_ExpenseSplitter;

public class ExpenseSplitter_Processor : IProcessor
{
    private readonly IFileExpense_Provider _repo;

    public ExpenseSplitter_Processor(IFileExpense_Provider repo)
    {
        _repo = repo;
    }

    public Payment[] SplitCosts()
    {
        var expenses = _repo.Load();
        return ExpenseSplitter_Core.SplitExpenses(expenses);
    }
} 