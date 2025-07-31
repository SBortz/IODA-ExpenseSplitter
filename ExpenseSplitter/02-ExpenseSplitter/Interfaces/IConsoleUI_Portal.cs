using ExpenseSplitter._02_ExpenseSplitter.DataContracts;
using ExpenseSplitter._99_IODA_BuildingBlocks;

namespace ExpenseSplitter._02_ExpenseSplitter.Interfaces;

public interface IConsoleUI_Portal : IPortal
{
    void Print(Payment[] payments);
}