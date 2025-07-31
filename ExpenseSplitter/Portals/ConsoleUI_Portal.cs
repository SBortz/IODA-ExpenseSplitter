using ExpenseSplitter.Core;

namespace ExpenseSplitter.Portals;

/// <summary>
/// Console UI - handles user interaction and display
/// </summary>
public class ConsoleUI_Portal : IPortal
{
    /// <summary>
    /// Print payments to console
    /// </summary>
    public void Print(Payment[] payments)
    {
        foreach (var p in payments)
        {
            Console.Write(p.name);
            
            if (p.amount < 0)
            {
                Console.Write(" pays ");
            }
            else
            {
                Console.Write(" receives ");
            }
            
            Console.WriteLine(Math.Abs(p.amount).ToString("0.00"));
        }
    }
} 