using ExpenseSplitter;
using ExpenseSplitter._01_Application;
using ExpenseSplitter._02_ExpenseSplitter;

Console.WriteLine("Loading expenses from expenses.txt...");
Console.WriteLine();

// Construction - wire up dependencies in 3-layer hierarchy
// Bottom Layer (3 cells): FileExpenseProvider, ConsoleUI_Portal, ValidationEngine_Core
var fileProvider = new FileExpens_Provider();
var consoleUI = new ConsoleUI_Portal();

// Middle Layer (2 cells): ExpenseProcessor_Processor, ReportGenerator_Portal
var processor = new ExpenseSplitter_Processor(fileProvider);
var reportGenerator = new ReportGenerator_Portal();

// Top Layer (1 cell): ExpenseSplitterApplication_Application
var app = new Application(consoleUI, processor, reportGenerator);

app.Run();