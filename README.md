# IODA Architecture Example - Expense Splitter

Dieses Projekt demonstriert die IODA-Architektur (Integration Operation Segregation Architecture) anhand eines einfachen Ausgaben-Splitters, wie in dem Artikel von Ralf Westphal beschrieben.

## Was ist IODA?

IODA ist ein Architekturmuster, das Anliegen in folgende Bereiche trennt:
- **Core**: Enthält die Domänenlogik und Geschäftsregeln
- **Portals**: Behandeln Benutzerinteraktion und Ein-/Ausgabe
- **Providers**: Verwalten externe Ressourcen und Datenzugriff
- **Integrations**: Koordinieren zwischen Core, Portals und Providers

## Schlüsselprinzipien

1. **Integration Operation Segregation Principle (IOSP)**: Trennt Funktionen, die Logik enthalten, von denen, die keine enthalten
2. **Keine funktionalen Abhängigkeiten**: Core-Module hängen nicht von Implementierungsdetails ab
3. **Rekursive Struktur**: Jede Komponente kann weiter in IODA-Komponenten zerlegt werden
4. **Sleepy Hollow Architecture**: Trennt "Kopf" (Construction/Application) vom "Körper" (Processor)

## Projektstruktur

```
ExpenseSplitter/
├── Core/                    # Domänenlogik (schwarzer Kreis)
│   ├── Payment.cs
│   ├── Expense.cs
│   └── Splitter_Core.cs
├── Providers/               # Externe Ressourcen (grünes Dreieck)
│   ├── IProvider.cs
│   └── ExpenseRepository.cs
├── Portals/                 # Benutzerinteraktion (blaues Quadrat)
│   ├── IPortal.cs
│   └── UI.cs
├── Processors/              # Koordinationslogik (mittlerer Abschnitt)
│   ├── IProcessor.cs
│   └── Processor.cs
├── Application/             # High-Level-Flow (zweiter Abschnitt)
│   └── Application.cs
├── Program.cs               # Construction (oberster Abschnitt)
└── expenses.txt             # Beispieldaten
```

## IODA-Flow

1. **Construction** (`Program.cs`): Dependencies werden zusammengefügt
2. **Application** (`Application.cs`): High-Level-Flow orchestriert
3. **Processor** (`Processor.cs`): Integriert Core-Logik mit Provider
4. **Portal** (`UI.cs`): Benutzerinteraktion und Ausgabe
5. **Core** (`Splitter_Core.cs`): Reine Domänenlogik
6. **Provider** (`ExpenseRepository.cs`): Datenzugriff abstrahiert

## Ausführen des Beispiels

### Voraussetzungen
- .NET 8.0 SDK

### Build und Run
```bash
cd ExpenseSplitter
dotnet build
dotnet run
```

### Erwartete Ausgabe
```
Alice receives 37.50
Bob pays 12.50
Charlie receives 12.50
David pays 37.50
```

## Architekturvorteile

Die IODA-Architektur demonstriert mehrere Schlüsselvorteile:

1. **Testbarkeit**: Jede Komponente kann isoliert getestet werden
2. **Wartbarkeit**: Klare Trennung der Anliegen
3. **Flexibilität**: Einfacher Austausch von Implementierungen
4. **Keine funktionalen Abhängigkeiten**: Core-Logik ist unabhängig von externen Anliegen

## Wie es funktioniert

1. **Construction**: `Program.cs` erstellt die Anwendung mit allen Dependencies
2. **Application**: `Application.Run()` startet den High-Level-Flow
3. **Processor**: `Processor.SplitCosts()` lädt Daten und wendet Core-Logik an
4. **Provider**: `ExpenseRepository.Load()` liest Ausgaben aus der Datei
5. **Core**: `Splitter_Core.Split()` berechnet die Aufteilung
6. **Portal**: `UI.Print()` zeigt die Ergebnisse an

Dieses Beispiel zeigt, wie IODA die Trennung von Anliegen ermöglicht und funktionale Abhängigkeiten eliminiert, was zu einer saubereren, testbareren und wartbareren Architektur führt. 