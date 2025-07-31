using ExpenseSplitter.Core;
using ExpenseSplitter.Processors;
using ExpenseSplitter.Providers;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests for the Processor component
/// Demonstrates integration testing with mocked providers
/// </summary>
public class ProcessorTests
{
    [Fact]
    public void SplitCosts_WithMockProvider_ShouldReturnCorrectPayments()
    {
        // Arrange
        var mockProvider = new MockExpenseProvider();
        var processor = new Processor(mockProvider);

        // Act
        var result = processor.SplitCosts();

        // Assert
        Assert.Equal(4, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");
        var charliePayment = result.First(p => p.name == "Charlie");
        var davidPayment = result.First(p => p.name == "David");

        Assert.Equal(37.50, alicePayment.amount, 2);
        Assert.Equal(-12.50, bobPayment.amount, 2);
        Assert.Equal(12.50, charliePayment.amount, 2);
        Assert.Equal(-37.50, davidPayment.amount, 2);
    }

    [Fact]
    public void SplitCosts_WithEmptyProvider_ShouldReturnEmptyArray()
    {
        // Arrange
        var emptyProvider = new EmptyExpenseProvider();
        var processor = new Processor(emptyProvider);

        // Act
        var result = processor.SplitCosts();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SplitCosts_WithSingleExpenseProvider_ShouldReturnEmptyArray()
    {
        // Arrange
        var singleProvider = new SingleExpenseProvider();
        var processor = new Processor(singleProvider);

        // Act
        var result = processor.SplitCosts();

        // Assert
        Assert.Empty(result);
    }
}

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