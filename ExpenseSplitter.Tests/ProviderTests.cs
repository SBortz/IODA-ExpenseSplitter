using ExpenseSplitter.DataContracts;
using ExpenseSplitter.Providers;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests for the FileExpenseProvider component
/// Demonstrates testing data access layer
/// </summary>
public class FileExpenseProviderTests
{
    [Fact]
    public void Load_WithValidFile_ShouldReturnCorrectExpenses()
    {
        // Arrange
        var provider = new FileExpenseProvider();

        // Act
        var result = provider.Load();

        // Assert
        Assert.Equal(4, result.Length);
        
        var aliceExpense = result.First(e => e.name == "Alice");
        var bobExpense = result.First(e => e.name == "Bob");
        var charlieExpense = result.First(e => e.name == "Charlie");
        var davidExpense = result.First(e => e.name == "David");

        Assert.Equal(100.0, aliceExpense.amount, 2);
        Assert.Equal(50.0, bobExpense.amount, 2);
        Assert.Equal(75.0, charlieExpense.amount, 2);
        Assert.Equal(25.0, davidExpense.amount, 2);
    }

    [Fact]
    public void Load_WithValidFile_ShouldParseDecimalValuesCorrectly()
    {
        // Arrange
        var provider = new FileExpenseProvider();

        // Act
        var result = provider.Load();

        // Assert
        foreach (var expense in result)
        {
            Assert.True(expense.amount >= 0); // Alle Beträge sollten positiv sein
            Assert.False(string.IsNullOrEmpty(expense.name)); // Namen sollten nicht leer sein
        }
    }

    [Fact]
    public void Load_WithValidFile_ShouldHaveCorrectTotalAmount()
    {
        // Arrange
        var provider = new FileExpenseProvider();

        // Act
        var result = provider.Load();

        // Assert
        var totalAmount = result.Sum(e => e.amount);
        Assert.Equal(250.0, totalAmount, 2); // 100 + 50 + 75 + 25 = 250
    }
} 