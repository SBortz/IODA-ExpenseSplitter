using ExpenseSplitter.BuildingBlocks;
using ExpenseSplitter.ExpenseSplitter.DataContracts;

namespace ExpenseSplitter.ExpenseSplitter.Interfaces;

public interface IConsoleUI_Portal : IPortal
{
    void Print(Payment[] payments);
}