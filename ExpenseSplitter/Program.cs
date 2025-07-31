using ExpenseSplitter;
using ExpenseSplitter.ExpenseSplitter;
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter;

Console.WriteLine("Loading expenses from expenses.txt...");
Console.WriteLine();

var fileProvider = new FileExpens_Provider();
var consoleUI = new ConsoleUI_Portal();
var currencyConverterProvider = new CurrencyConverter_Provider();
var currencyConverterProcessor = new CurrencyConverter_Processor(currencyConverterProvider);
var processor = new ExpenseSplitter_Processor(fileProvider, currencyConverterProcessor);
var reportGenerator = new ReportGenerator_Portal();
var app = new Application(consoleUI, processor, reportGenerator);

app.Run();