using ExpenseSplitter._03_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._03_ExpenseSplitter.Interfaces;

public interface IConsoleUI_Portal : IPortal
{
    void Print(Payment[] payments);
}