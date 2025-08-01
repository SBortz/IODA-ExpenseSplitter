# IODA Expense Splitter

Ein Beispielprojekt zur Demonstration der **IODA Architecture** (Integration Operation Segregation Architecture) basierend auf dem Artikel von Ralf Westphal: [IODA Architecture](https://ralfwestphal.substack.com/p/ioda-architecture).

## Was ist IODA?

IODA (Integration Operation Segregation Architecture) ist ein moderner Architekturansatz, der das Problem der **funktionalen Abhängigkeiten** (Functional Dependencies) löst, das in traditionellen Architekturmustern wie MVC, Layered Architecture, Hexagonal Architecture und Clean Architecture auftritt.

### Das Problem funktionaler Abhängigkeiten

Funktionale Abhängigkeiten entstehen, wenn Module nicht nur Daten austauschen, sondern sich gegenseitig in ihrer Funktionalität beeinflussen. Dies führt zu:

- **Schwierigkeiten beim Testen**: Module können nicht isoliert getestet werden
- **Eingeschränkte Wartbarkeit**: Änderungen in einem Modul beeinflussen andere
- **Komplexe Abhängigkeitsgraphen**: Zirkuläre Abhängigkeiten und enge Kopplung

### Die IODA-Lösung

IODA löst dieses Problem durch die **Trennung von Integrations- und Operationslogik**:

- **Operationslogik**: Reine, funktionale Logik ohne Seiteneffekte
- **Integrationslogik**: Koordination zwischen verschiedenen Modulen und externen Systemen

## IODA-Bausteine

IODA definiert vier grundlegende Bausteine:

### 1. Core (Kern)
- Enthält die **reine Geschäftslogik**
- Keine Abhängigkeiten zu anderen Modulen
- Funktional rein und deterministisch
- Einfach zu testen

### 2. Portal (Portal)
- **Eingangspunkt** für externe Interaktionen
- Handhabt Benutzerinteraktionen (UI, API, etc.)
- Keine Geschäftslogik

### 3. Provider (Anbieter)
- **Ausgangspunkt** für externe Ressourcen
- Zugriff auf Datenbanken, APIs, Dateisysteme
- Keine Geschäftslogik

### 4. Processor (Verarbeiter)
- **Koordiniert** Core, Portal und Provider
- Orchestriert den Datenfluss
- Enthält Integrationslogik

## Sleepy Hollow Architecture

Dieses Projekt implementiert auch die **Sleepy Hollow Architecture**, eine Verfeinerung von IODA:

- **Head (Kopf)**: Portal-Komponenten - schwer zu testen
- **Body (Körper)**: Processor und Core - einfach zu testen
- **Construction**: Dependency Injection und Objektinstanziierung

## Projektstruktur und Abhängigkeitsgraph

```
IODA-ExpenseSplitter/
├── ExpenseSplitter/
│   ├── Application.cs                    # Construction Layer
│   ├── Program.cs                        # Entry Point
│   ├── BuildingBlocks/                   # IODA Interfaces
│   │   ├── IPortal.cs
│   │   ├── IProcessor.cs
│   │   └── IProvider.cs
│   └── ExpenseSplitter/
│       ├── ConsoleUI_Portal.cs           # Portal (Head)
│       ├── ExpenseSplitter_Processor.cs  # Processor (Body)
│       ├── ExpenseSplitter_Core.cs       # Core (Body)
│       ├── FileExpens_Provider.cs        # Provider (Body)
│       ├── ReportGenerator_Portal.cs     # Portal (Head)
│       └── CurrencyConverter/
│           ├── CurrencyConverter_Core.cs        # Core (Body)
│           ├── CurrencyConverter_Processor.cs   # Processor (Body)
│           └── CurrencyConverter_Provider.cs    # Provider (Body)
└── ExpenseSplitter.Tests/
    ├── CurrencyConverter_Core_Tests.cs   # Core Tests
    └── Splitter_Core_Tests.cs            # Core Tests
```

### Abhängigkeitsfluss

```
Program.cs
    ↓
Application.cs (Construction)
    ↓
ConsoleUI_Portal (Portal) ←→ ExpenseSplitter_Processor (Processor)
                              ↓
                              FileExpens_Provider (Provider)
                              ↓
                              CurrencyConverter_Processor (Processor)
                              ↓
                              CurrencyConverter_Provider (Provider)
                              ↓
                              CurrencyConverter_Core (Core)
                              ↓
                              ExpenseSplitter_Core (Core)
```

### IODA-Prinzipien in diesem Projekt

#### 1. Core-Komponenten (Reine Operationslogik)
- **`ExpenseSplitter_Core`**: Berechnet die Aufteilung der Ausgaben
- **`CurrencyConverter_Core`**: Konvertiert Währungen (funktional rein)

```csharp
// Beispiel: Reine Funktion ohne Seiteneffekte
public static Payment[] SplitExpenses(EurExpense[] expenses)
{
    var av = expenses.Select(x => x.amount).Average();
    return expenses
        .Where(x => x.amount != av)
        .Select(x => new Payment(x.name, x.amount - av))
        .ToArray();
}
```

#### 2. Processor-Komponenten (Integrationslogik)
- **`ExpenseSplitter_Processor`**: Koordiniert Datenfluss zwischen Provider und Core
- **`CurrencyConverter_Processor`**: Orchestriert Währungskonvertierung

```csharp
// Beispiel: Integration ohne funktionale Abhängigkeiten
public Payment[] SplitCosts()
{
    var currencyExpenses = _repo.LoadExpenses();
    var expensesEuro = _currencyConverter.ConvertToEur(currencyExpenses);
    return ExpenseSplitter_Core.SplitExpenses(expensesEuro);
}
```

#### 3. Portal-Komponenten (Benutzerinteraktion)
- **`ConsoleUI_Portal`**: Konsolenausgabe
- **`ReportGenerator_Portal`**: Berichtgenerierung

#### 4. Provider-Komponenten (Externe Ressourcen)
- **`FileExpens_Provider`**: Dateizugriff
- **`CurrencyConverter_Provider`**: Währungs-API (Mock)

## Vorteile der IODA-Architektur

### 1. Einfaches Testen
- **Core-Komponenten** sind funktional rein und einfach zu testen
- **Provider** können gemockt werden
- **Processor** können isoliert getestet werden

```csharp
[Fact]
public void DemonstrateFunctionalPurity()
{
    // Core-Funktionen sind deterministisch
    var result1 = CurrencyConverter_Core.ConvertToEur(expenses, rates);
    var result2 = CurrencyConverter_Core.ConvertToEur(expenses, rates);
    Assert.True(areIdentical); // Immer identisch
}
```

### 2. Klare Trennung der Verantwortlichkeiten
- **Operationslogik** ist von **Integrationslogik** getrennt
- Keine zirkulären Abhängigkeiten
- Einfache Wartung und Erweiterung

### 3. Skalierbarkeit
- IODA ist **rekursiv** anwendbar
- Große Systeme können in kleinere IODA-Zellen aufgeteilt werden
- Jede Zelle folgt dem gleichen Muster

## Ausführung

```bash
# Projekt kompilieren
dotnet build

# Tests ausführen
dotnet test

# Anwendung starten
dotnet run --project ExpenseSplitter
```

## Datei `expenses.txt`

Erstellen Sie eine Datei `expenses.txt` im Projektverzeichnis:

```
Alice,100,USD
Bob,50,EUR
Charlie,200,GBP
```

## Fazit

Dieses Projekt demonstriert, wie IODA die Probleme traditioneller Architekturmuster löst:

1. **Keine funktionalen Abhängigkeiten** zwischen Modulen
2. **Einfache Testbarkeit** durch funktional reine Core-Komponenten
3. **Klare Trennung** zwischen Operations- und Integrationslogik
4. **Skalierbare Architektur** für kleine und große Systeme

IODA bietet einen modernen Ansatz für saubere, wartbare und testbare Softwarearchitektur, der über die traditionellen Patterns hinausgeht.

## Weitere Ressourcen

- [IODA Architecture Artikel](https://ralfwestphal.substack.com/p/ioda-architecture) von Ralf Westphal
- [Flow Design Programming](https://www.amazon.de/Flow-Design-Programming-Ralf-Westphal/dp/3864909054) - Buch über IODA und verwandte Konzepte 