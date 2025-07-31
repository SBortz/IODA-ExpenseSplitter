using ExpenseSplitter;
using ExpenseSplitter._01_Application;
using ExpenseSplitter._02_ExpenseSplitter;
using ExpenseSplitter._02_ExpenseSplitter.CurrencyConverter;

Console.WriteLine("Loading expenses from expenses.txt...");
Console.WriteLine();

// Construction - wire up dependencies in 3-layer hierarchy
// Bottom Layer (4 cells): FileExpenseProvider, ConsoleUI_Portal, CurrencyConverter_Provider
var fileProvider = new FileExpens_Provider();
var consoleUI = new ConsoleUI_Portal();
var currencyConverterProvider = new CurrencyConverter_Provider();

// Middle Layer (3 cells): ExpenseProcessor_Processor, ReportGenerator_Portal, CurrencyConverter_Processor
var currencyConverterProcessor = new CurrencyConverter_Processor(currencyConverterProvider);
var processor = new ExpenseSplitter_Processor(fileProvider, currencyConverterProcessor);
var reportGenerator = new ReportGenerator_Portal();

// Top Layer (1 cell): ExpenseSplitterApplication_Application
var app = new Application(consoleUI, processor, reportGenerator);

app.Run();