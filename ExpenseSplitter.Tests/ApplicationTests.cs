using ExpenseSplitter.Application;
using ExpenseSplitter.Core;
using ExpenseSplitter.DataContracts;
using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Tests for the ExpenseSplitterApplication_Application component
/// Demonstrates testing the high-level flow
/// </summary>
public class ExpenseSplitterApplicationTests
{
    [Fact]
    public void Run_WithMockComponents_ShouldCallPortalWithCorrectPayments()
    {
        // Arrange
        var mockPortal = new MockPortal();
        var mockProcessor = new MockProcessor();
        var application = new ExpenseSplitter.Application.Application(mockPortal, mockProcessor, new ReportGenerator_Portal());

        // Act
        application.Run();

        // Assert
        Assert.True(mockPortal.PrintWasCalled);
        Assert.Equal(4, mockPortal.ReceivedPayments.Length);
        
        var alicePayment = mockPortal.ReceivedPayments.First(p => p.name == "Alice");
        var bobPayment = mockPortal.ReceivedPayments.First(p => p.name == "Bob");
        var charliePayment = mockPortal.ReceivedPayments.First(p => p.name == "Charlie");
        var davidPayment = mockPortal.ReceivedPayments.First(p => p.name == "David");

        Assert.Equal(37.50, alicePayment.amount, 2);
        Assert.Equal(-12.50, bobPayment.amount, 2);
        Assert.Equal(12.50, charliePayment.amount, 2);
        Assert.Equal(-37.50, davidPayment.amount, 2);
    }

    [Fact]
    public void Run_WithEmptyProcessor_ShouldCallPortalWithEmptyPayments()
    {
        // Arrange
        var mockPortal = new MockPortal();
        var emptyProcessor = new EmptyProcessor();
        var application = new ExpenseSplitter.Application.Application(mockPortal, emptyProcessor, new ReportGenerator_Portal());

        // Act
        application.Run();

        // Assert
        Assert.True(mockPortal.PrintWasCalled);
        Assert.Empty(mockPortal.ReceivedPayments);
    }
}

/// <summary>
/// Mock portal for testing - captures the payments passed to Print method
/// </summary>
public class MockPortal : IPortal
{
    public bool PrintWasCalled { get; private set; }
    public Payment[] ReceivedPayments { get; private set; } = Array.Empty<Payment>();

    public void Print(Payment[] payments)
    {
        PrintWasCalled = true;
        ReceivedPayments = payments;
    }
}

/// <summary>
/// Mock processor for testing - returns the same data as the real processor
/// </summary>
public class MockProcessor : IProcessor
{
    public Payment[] SplitCosts()
    {
        return new[]
        {
            new Payment("Alice", 37.50),
            new Payment("Bob", -12.50),
            new Payment("Charlie", 12.50),
            new Payment("David", -37.50)
        };
    }
}

/// <summary>
/// Mock processor that returns empty array
/// </summary>
public class EmptyProcessor : IProcessor
{
    public Payment[] SplitCosts()
    {
        return new Payment[0];
    }
} 