using ExpenseSplitter.ExpenseSplitter.CurrencyConverter;
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests to demonstrate the functional purity of CurrencyConverter_Core
/// </summary>
public class CurrencyConverter_Core_Tests
{
    /// <summary>
    /// Demonstrate that the core function is pure and deterministic
    /// </summary>
    [Fact]
    public void DemonstrateFunctionalPurity()
    {
        // Test data
        var expenses = new CurrencyExpense[]
        {
            new("Alice", 100.0, "USD"),
            new("Bob", 50.0, "EUR"),
            new("Charlie", 200.0, "GBP")
        };
        
        var exchangeRates = new Dictionary<string, double>
        {
            { "USD", 0.85 },
            { "GBP", 1.18 }
        };
        
        // First call
        var result1 = CurrencyConverter_Core.ConvertToEur(expenses, exchangeRates);
        
        // Second call with same inputs
        var result2 = CurrencyConverter_Core.ConvertToEur(expenses, exchangeRates);
        
        // Third call with same inputs
        var result3 = CurrencyConverter_Core.ConvertToEur(expenses, exchangeRates);
        
        // All results should be identical (deterministic)
        var areIdentical = result1.Length == result2.Length && result2.Length == result3.Length &&
                          result1.Zip(result2).All(pair => pair.First.name == pair.Second.name && 
                                                          Math.Abs(pair.First.amount - pair.Second.amount) < 0.001) &&
                          result2.Zip(result3).All(pair => pair.First.name == pair.Second.name && 
                                                          Math.Abs(pair.First.amount - pair.Second.amount) < 0.001);
        
        // Assertions
        Assert.True(areIdentical, "Results should be identical for same inputs");
        Assert.Equal(3, result1.Length);
        Assert.Equal(3, result2.Length);
        Assert.Equal(3, result3.Length);
        
        // Demonstrate no side effects by checking that inputs are unchanged
        Assert.Equal(100.0, expenses[0].amount);
        Assert.Equal(50.0, expenses[1].amount);
        Assert.Equal(200.0, expenses[2].amount);
        Assert.Equal(0.85, exchangeRates["USD"]);
        Assert.Equal(1.18, exchangeRates["GBP"]);
    }
    
    /// <summary>
    /// Test that EUR expenses are not converted
    /// </summary>
    [Fact]
    public void EurExpensesShouldNotBeConverted()
    {
        var expenses = new CurrencyExpense[]
        {
            new("Alice", 100.0, "EUR"),
            new("Bob", 50.0, "EUR")
        };
        
        var exchangeRates = new Dictionary<string, double>();
        
        var result = CurrencyConverter_Core.ConvertToEur(expenses, exchangeRates);
        
        Assert.Equal(100.0, result[0].amount);
        Assert.Equal(50.0, result[1].amount);
    }
    
    /// <summary>
    /// Test that unknown currencies default to 1.0 rate
    /// </summary>
    [Fact]
    public void UnknownCurrenciesShouldDefaultToOne()
    {
        var expenses = new CurrencyExpense[]
        {
            new("Alice", 100.0, "UNKNOWN")
        };
        
        var exchangeRates = new Dictionary<string, double>();
        
        var result = CurrencyConverter_Core.ConvertToEur(expenses, exchangeRates);
        
        Assert.Equal(100.0, result[0].amount); // Default rate is 1.0
    }
    
    /// <summary>
    /// Test null inputs
    /// </summary>
    [Fact]
    public void NullInputsShouldReturnEmptyArray()
    {
        var result1 = CurrencyConverter_Core.ConvertToEur(null!, new Dictionary<string, double>());
        var result2 = CurrencyConverter_Core.ConvertToEur(new CurrencyExpense[0], null!);
        
        Assert.Empty(result1);
        Assert.Empty(result2);
    }
} 