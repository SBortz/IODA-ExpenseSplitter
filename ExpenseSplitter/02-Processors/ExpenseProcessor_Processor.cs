using ExpenseSplitter._03_ExpenseSplitter;
using ExpenseSplitter._03_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_Processors;

public class ExpenseProcessor_Processor : IProcessor
{
    private readonly IFileExpense_Provider _repo;

    public ExpenseProcessor_Processor(IFileExpense_Provider repo)
    {
        _repo = repo;
    }

    public Payment[] SplitCosts()
    {
        var expenses = _repo.Load();
        return ExpenseSplitter_Core.SplitExpenses(expenses);
    }
} 