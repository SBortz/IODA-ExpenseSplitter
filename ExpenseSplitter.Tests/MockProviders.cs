using ExpenseSplitter.Core;
using ExpenseSplitter.DataContracts;
using ExpenseSplitter.Providers;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Mock provider for testing - returns the same data as expenses.txt
/// </summary>
public class MockExpenseProvider : IProvider
{
    public Expense[] Load()
    {
        return new[]
        {
            new Expense("Alice", 100.0),
            new Expense("Bob", 50.0),
            new Expense("Charlie", 75.0),
            new Expense("David", 25.0)
        };
    }
}

/// <summary>
/// Mock provider that returns empty array
/// </summary>
public class EmptyExpenseProvider : IProvider
{
    public Expense[] Load()
    {
        return new Expense[0];
    }
}

/// <summary>
/// Mock provider that returns single expense
/// </summary>
public class SingleExpenseProvider : IProvider
{
    public Expense[] Load()
    {
        return new[] { new Expense("Alice", 100.0) };
    }
} 