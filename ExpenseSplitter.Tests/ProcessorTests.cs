using ExpenseSplitter.Core;
using ExpenseSplitter.DataContracts;
using ExpenseSplitter.Processors;
using ExpenseSplitter.Providers;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests for the ExpenseProcessor_Processor component
/// Demonstrates integration testing with mocked providers
/// </summary>
public class ExpenseProcessorTests
{
    [Fact]
    public void SplitCosts_WithMockProvider_ShouldReturnCorrectPayments()
    {
        // Arrange
        var mockProvider = new MockExpenseProvider();
        var processor = new ExpenseProcessor_Processor(mockProvider, new ValidationEngine_Core());

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
    public void SplitCosts_WithEmptyProvider_ShouldThrowValidationException()
    {
        // Arrange
        var emptyProvider = new EmptyExpenseProvider();
        var processor = new ExpenseProcessor_Processor(emptyProvider, new ValidationEngine_Core());

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => processor.SplitCosts());
        Assert.Contains("No expenses provided", exception.Message);
    }

    [Fact]
    public void SplitCosts_WithSingleExpenseProvider_ShouldThrowValidationException()
    {
        // Arrange
        var singleProvider = new SingleExpenseProvider();
        var processor = new ExpenseProcessor_Processor(singleProvider, new ValidationEngine_Core());

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => processor.SplitCosts());
        Assert.Contains("At least 2 expenses are required for splitting", exception.Message);
    }
} 