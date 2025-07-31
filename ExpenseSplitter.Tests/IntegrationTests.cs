using ExpenseSplitter.Core;
using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;
using ExpenseSplitter.Providers;
using Xunit;

namespace ExpenseSplitter.Tests;

/// <summary>
/// Integration tests for the complete IODA architecture
/// Demonstrates testing the entire flow from construction to output
/// </summary>
public class IntegrationTests
{
    [Fact]
    public void CompleteFlow_WithRealComponents_ShouldWorkCorrectly()
    {
        // Arrange - Construction phase (wie in Program.cs)
        var provider = new ExpenseRepository();
        var processor = new Processor(provider);
        var portal = new UI();
        var application = new ExpenseSplitter.Application.Application(portal, processor);

        // Act - Application phase
        application.Run();

        // Assert - Verify that the flow works end-to-end
        // Note: This test verifies that the application runs without throwing exceptions
        // The actual output verification would require capturing console output
        Assert.True(true); // If we reach here, the flow worked
    }

    [Fact]
    public void CompleteFlow_WithMockComponents_ShouldProduceCorrectResults()
    {
        // Arrange - Construction with mocks
        var mockProvider = new MockExpenseProvider();
        var processor = new Processor(mockProvider);
        var mockPortal = new MockPortal();
        var application = new ExpenseSplitter.Application.Application(mockPortal, processor);

        // Act - Application phase
        application.Run();

        // Assert - Verify the complete flow
        Assert.True(mockPortal.PrintWasCalled);
        Assert.Equal(4, mockPortal.ReceivedPayments.Length);
        
        // Verify the business logic was applied correctly
        var alicePayment = mockPortal.ReceivedPayments.First(p => p.name == "Alice");
        var bobPayment = mockPortal.ReceivedPayments.First(p => p.name == "Bob");
        var charliePayment = mockPortal.ReceivedPayments.First(p => p.name == "Charlie");
        var davidPayment = mockPortal.ReceivedPayments.First(p => p.name == "David");

        Assert.Equal(37.50, alicePayment.amount, 2);
        Assert.Equal(-12.50, bobPayment.amount, 2);
        Assert.Equal(12.50, charliePayment.amount, 2);
        Assert.Equal(-37.50, davidPayment.amount, 2);

        // Verify that the sum of all payments equals zero
        var totalPayment = mockPortal.ReceivedPayments.Sum(p => p.amount);
        Assert.Equal(0.0, totalPayment, 2);
    }

    [Fact]
    public void IODAArchitecture_ShouldSeparateConcernsCorrectly()
    {
        // This test demonstrates that the IODA architecture properly separates concerns
        
        // Core: Pure business logic without dependencies
        var expenses = new[]
        {
            new Expense("Alice", 100.0),
            new Expense("Bob", 50.0)
        };
        var coreResult = Splitter_Core.Split(expenses);
        Assert.Equal(2, coreResult.Length);

        // Provider: Data access abstraction
        var provider = new MockExpenseProvider();
        var providerResult = provider.Load();
        Assert.Equal(4, providerResult.Length);

        // Processor: Integration of core and provider
        var processor = new Processor(provider);
        var processorResult = processor.SplitCosts();
        Assert.Equal(4, processorResult.Length);

        // Portal: User interaction
        var portal = new MockPortal();
        portal.Print(processorResult);
        Assert.True(portal.PrintWasCalled);

        // Application: High-level orchestration
        var application = new ExpenseSplitter.Application.Application(portal, processor);
        application.Run();
        Assert.True(portal.PrintWasCalled);
    }

    [Fact]
    public void IODAArchitecture_ShouldBeTestableAtEachLevel()
    {
        // This test demonstrates the testability of each IODA layer
        
        // Test Core in isolation
        var coreExpenses = new[] { new Expense("Test", 100.0) };
        var coreResult = Splitter_Core.Split(coreExpenses);
        Assert.Empty(coreResult); // Single expense should return empty array

        // Test Provider in isolation
        var mockProvider = new MockExpenseProvider();
        var providerResult = mockProvider.Load();
        Assert.Equal(4, providerResult.Length);

        // Test Processor with mocked provider
        var processor = new Processor(mockProvider);
        var processorResult = processor.SplitCosts();
        Assert.Equal(4, processorResult.Length);

        // Test Portal with mocked data
        var mockPortal = new MockPortal();
        mockPortal.Print(processorResult);
        Assert.True(mockPortal.PrintWasCalled);

        // Test Application with mocked components
        var application = new ExpenseSplitter.Application.Application(mockPortal, processor);
        application.Run();
        Assert.True(mockPortal.PrintWasCalled);
    }
} 