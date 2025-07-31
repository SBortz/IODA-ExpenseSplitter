using ExpenseSplitter.Application;
using ExpenseSplitter.Core;
using ExpenseSplitter.Portals;
using ExpenseSplitter.Processors;
using ExpenseSplitter.Providers;

Console.WriteLine("=== IODA Architecture Example - Expense Splitter ===");
Console.WriteLine("Demonstrating 3-Layer Software Cell Hierarchy");
Console.WriteLine("Loading expenses from expenses.txt...");
Console.WriteLine();

// Construction - wire up dependencies in 3-layer hierarchy
// Bottom Layer (3 cells): FileExpenseProvider, ConsoleUI_Portal, ValidationEngine_Core
var fileProvider = new FileExpenseProvider();
var consoleUI = new ConsoleUI_Portal();
var validationEngine = new ValidationEngine_Core();

// Middle Layer (2 cells): ExpenseProcessor_Processor, ReportGenerator_Portal
var processor = new ExpenseProcessor_Processor(fileProvider, validationEngine);
var reportGenerator = new ReportGenerator_Portal();

// Top Layer (1 cell): ExpenseSplitterApplication_Application
var app = new ExpenseSplitterApplication_Application(consoleUI, processor, reportGenerator);

app.Run();

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey(); 