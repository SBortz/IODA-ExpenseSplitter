using ExpenseSplitter.ExpenseSplitter;
using ExpenseSplitter.ExpenseSplitter.DataContracts;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests for the ExpenseSplitter_Core functional core
/// Demonstrates how easy it is to test pure business logic without external dependencies
/// </summary>
public class ExpenseSplitter_Core_Tests
{
    [Fact]
    public void Split_EqualExpenses_ShouldReturnEmptyArray()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 100.0),
            new EurExpense("Bob", 100.0),
            new EurExpense("Charlie", 100.0)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Split_UnequalExpenses_ShouldCalculateCorrectPayments()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 100.0),  // +37.50 (über Durchschnitt)
            new EurExpense("Bob", 50.0),     // -12.50 (unter Durchschnitt)
            new EurExpense("Charlie", 75.0), // +12.50 (über Durchschnitt)
            new EurExpense("David", 25.0)    // -37.50 (unter Durchschnitt)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

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
    public void Split_OnePersonPaysEverything_ShouldReturnCorrectPayments()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 300.0),  // Zahlt alles
            new EurExpense("Bob", 0.0),      // Zahlt nichts
            new EurExpense("Charlie", 0.0)   // Zahlt nichts
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(3, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");
        var charliePayment = result.First(p => p.name == "Charlie");

        Assert.Equal(200.0, alicePayment.amount, 2);  // Alice erhält 200€ zurück
        Assert.Equal(-100.0, bobPayment.amount, 2);   // Bob zahlt 100€
        Assert.Equal(-100.0, charliePayment.amount, 2); // Charlie zahlt 100€
    }

    [Fact]
    public void Split_OnePersonReceivesEverything_ShouldReturnCorrectPayments()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 0.0),    // Zahlt nichts
            new EurExpense("Bob", 0.0),      // Zahlt nichts
            new EurExpense("Charlie", 300.0) // Zahlt alles
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(3, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");
        var charliePayment = result.First(p => p.name == "Charlie");

        Assert.Equal(-100.0, alicePayment.amount, 2);  // Alice zahlt 100€
        Assert.Equal(-100.0, bobPayment.amount, 2);    // Bob zahlt 100€
        Assert.Equal(200.0, charliePayment.amount, 2); // Charlie erhält 200€ zurück
    }

    [Fact]
    public void Split_ComplexScenario_ShouldCalculateCorrectPayments()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 150.0),   // +50 (über Durchschnitt)
            new EurExpense("Bob", 80.0),      // -20 (unter Durchschnitt)
            new EurExpense("Charlie", 120.0), // +20 (über Durchschnitt)
            new EurExpense("David", 70.0),    // -30 (unter Durchschnitt)
            new EurExpense("Eve", 100.0)      // 0 (genau Durchschnitt)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(5, result.Length); // Alle 5 Personen sollten im Ergebnis sein
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");
        var charliePayment = result.First(p => p.name == "Charlie");
        var davidPayment = result.First(p => p.name == "David");

        Assert.Equal(46.0, alicePayment.amount, 2);
        Assert.Equal(-24.0, bobPayment.amount, 2);
        Assert.Equal(16.0, charliePayment.amount, 2);
        Assert.Equal(-34.0, davidPayment.amount, 2);
        
        // Eve sollte im Ergebnis sein (auch wenn Betrag 0)
        var evePayment = result.First(p => p.name == "Eve");
        Assert.Equal(-4.0, evePayment.amount, 2);
    }

    [Fact]
    public void Split_EmptyExpenses_ShouldReturnEmptyArray()
    {
        // Arrange
        var expenses = new EurExpense[0];

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Split_SingleExpense_ShouldReturnEmptyArray()
    {
        // Arrange
        var expenses = new[] { new EurExpense("Alice", 100.0) };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Split_DecimalPrecision_ShouldHandleCorrectly()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 100.33),
            new EurExpense("Bob", 50.67),
            new EurExpense("Charlie", 75.00)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(3, result.Length);
        
        // Summe aller Payments sollte 0 sein (Rundungsfehler berücksichtigen)
        var totalPayment = result.Sum(p => p.amount);
        Assert.Equal(0.0, totalPayment, 2);
    }

    [Fact]
    public void Split_LargeNumbers_ShouldHandleCorrectly()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 1000000.0),
            new EurExpense("Bob", 500000.0),
            new EurExpense("Charlie", 750000.0)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(2, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");

        Assert.Equal(250000.0, alicePayment.amount, 2);
        Assert.Equal(-250000.0, bobPayment.amount, 2);
    }

    [Fact]
    public void Split_NegativeExpenses_ShouldHandleCorrectly()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", -100.0),
            new EurExpense("Bob", 200.0),
            new EurExpense("Charlie", 50.0)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(2, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");

        Assert.Equal(-150.0, alicePayment.amount, 2);
        Assert.Equal(150.0, bobPayment.amount, 2);
    }

    [Fact]
    public void Split_AllSameAmountExceptOne_ShouldReturnCorrectPayments()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 100.0),
            new EurExpense("Bob", 100.0),
            new EurExpense("Charlie", 100.0),
            new EurExpense("David", 200.0) // Zahlt 100€ mehr
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        Assert.Equal(4, result.Length);
        
        var alicePayment = result.First(p => p.name == "Alice");
        var bobPayment = result.First(p => p.name == "Bob");
        var charliePayment = result.First(p => p.name == "Charlie");
        var davidPayment = result.First(p => p.name == "David");

        Assert.Equal(-25.0, alicePayment.amount, 2);
        Assert.Equal(-25.0, bobPayment.amount, 2);
        Assert.Equal(-25.0, charliePayment.amount, 2);
        Assert.Equal(75.0, davidPayment.amount, 2);
    }

    [Fact]
    public void Split_TotalPaymentsShouldEqualZero()
    {
        // Arrange
        var expenses = new[]
        {
            new EurExpense("Alice", 150.0),
            new EurExpense("Bob", 80.0),
            new EurExpense("Charlie", 120.0),
            new EurExpense("David", 70.0),
            new EurExpense("Eve", 100.0)
        };

        // Act
        var result = ExpenseSplitter_Core.SplitExpenses(expenses);

        // Assert
        var totalPayment = result.Sum(p => p.amount);
        Assert.Equal(0.0, totalPayment, 2); // Summe aller Payments sollte 0 sein
    }
} 