using ExpenseSplitter.Application;
using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;
using ExpenseSplitter.Providers;

Console.WriteLine("=== IODA Architecture Example - Expense Splitter ===");
Console.WriteLine("Loading expenses from expenses.txt...");
Console.WriteLine();

// Construction - wire up dependencies
var app = new Application(
    new UI(),
    new Processor(new ExpenseRepository())
);

app.Run();

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey(); 