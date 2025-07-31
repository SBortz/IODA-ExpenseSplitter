# CurrencyConverter - IODA-Architektur mit funktionaler Reinheit

## 🎯 Übersicht

Die CurrencyConverter-Software-Zelle implementiert Währungskonvertierung nach dem IODA-Prinzip mit funktionaler Reinheit des Core-Bausteins.

## 🏗️ Projektstruktur

```
IODA-ExpenseSplitter/
├── ExpenseSplitter/
│   ├── BuildingBlocks/
│   │   ├── IProcessor.cs
│   │   ├── IPortal.cs
│   │   └── IProvider.cs
│   ├── ExpenseSplitter/
│   │   ├── CurrencyConverter/
│   │   │   ├── DataContracts/
│   │   │   │   ├── CurrencyExpense.cs
│   │   │   │   └── ExchangeRate.cs
│   │   │   ├── Interfaces/
│   │   │   │   ├── ICurrencyConverter_Provider.cs
│   │   │   │   └── ICurrencyConverter_Processor.cs
│   │   │   ├── CurrencyConverter_Core.cs
│   │   │   ├── CurrencyConverter_Processor.cs
│   │   │   └── CurrencyConverter_Provider.cs
│   │   ├── DataContracts/
│   │   ├── Interfaces/
│   │   └── ...
│   ├── Application.cs
│   └── Program.cs
└── ExpenseSplitter.Tests/
    └── CurrencyConverter_Core_Tests.cs
```

## 🏗️ Architektur

```
CurrencyConverter_Processor (Orchestrierung)
    ↓
CurrencyConverter_Core (Funktional pur)
    ↑
CurrencyConverter_Provider (Datenbeschaffung + Seiteneffekte)
```

## 📁 Namespaces

- **BuildingBlocks**: `ExpenseSplitter.BuildingBlocks`
- **CurrencyConverter**: `ExpenseSplitter.ExpenseSplitter.CurrencyConverter`
- **DataContracts**: `ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts`
- **Interfaces**: `ExpenseSplitter.ExpenseSplitter.CurrencyConverter.Interfaces`
- **Tests**: `ExpenseSplitter.Tests`

## ✅ Funktionale Reinheit

### Core-Baustein (Funktional pur)
```csharp
public static EurExpense[] ConvertToEur(CurrencyExpense[] currencyExpenses, Dictionary<string, double> exchangeRates)
{
    // ✅ Keine externen Aufrufe
    // ✅ Keine Seiteneffekte
    // ✅ Deterministisch
    // ✅ Nur lokale Berechnungen
}
```

### Provider (Seiteneffekte isoliert)
```csharp
public Dictionary<string, double> GetExchangeRatesForExpenses(CurrencyExpense[] currencyExpenses)
{
    // Extrahiert eindeutige Währungen und holt Wechselkurse
    // ⚠️ Seiteneffekte: API-Aufrufe, Thread.Sleep()
}
```

### Processor (Orchestrierung)
```csharp
public EurExpense[] ConvertToEur(CurrencyExpense[] currencyExpenses)
{
    // 1. Hole Wechselkurse vom Provider (Seiteneffekte)
    var exchangeRates = _exchangeRateProvider.GetExchangeRatesForExpenses(currencyExpenses);
    
    // 2. Rufe funktional reinen Core auf
    return CurrencyConverter_Core.ConvertToEur(currencyExpenses, exchangeRates);
}
```

## 🧪 Tests

Tests befinden sich im `ExpenseSplitter.Tests`-Projekt:

```csharp
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter;
using ExpenseSplitter.ExpenseSplitter.CurrencyConverter.DataContracts;
using Xunit;

namespace ExpenseSplitter.Tests;

public class CurrencyConverter_Core_Tests
{
    [Fact]
    public void DemonstrateFunctionalPurity()
    {
        // Test der funktionalen Reinheit
    }
}
```

### Test-Kategorien:
- **Funktionale Reinheit**: Nachweis der Deterministik und fehlender Seiteneffekte
- **EUR-Konvertierung**: Test dass EUR-Ausgaben nicht konvertiert werden
- **Unbekannte Währungen**: Test des Default-Verhaltens
- **Null-Eingaben**: Test der Robustheit

## 🎯 IODA-Prinzip eingehalten

- **Core**: Funktional pur, enthält nur Geschäftslogik
- **Processor**: Orchestriert Core und Provider
- **Provider**: Handhabt Datenbeschaffung und externe Abhängigkeiten
- **Interfaces**: Definieren klare Verträge
- **DataContracts**: Immutable Datenstrukturen

## 🚀 Verwendung

```csharp
// Dependency Injection
var currencyConverterProvider = new CurrencyConverter_Provider();
var currencyConverterProcessor = new CurrencyConverter_Processor(currencyConverterProvider);

// Verwendung
var currencyExpenses = fileProvider.LoadExpenses();
var eurExpenses = currencyConverterProcessor.ConvertToEur(currencyExpenses);
```

## 📊 Unterstützte Währungen

- **USD**: 1 USD = 0.85 EUR
- **GBP**: 1 GBP = 1.18 EUR
- **CHF**: 1 CHF = 0.92 EUR
- **JPY**: 1 JPY = 0.007 EUR
- **EUR**: Keine Konvertierung nötig
- **Unbekannte Währungen**: Default zu 1.0 (keine Konvertierung)

## 🔧 Ausführung

```bash
# Tests ausführen
cd ExpenseSplitter.Tests
dotnet test

# Anwendung ausführen
cd ExpenseSplitter
dotnet run
``` 