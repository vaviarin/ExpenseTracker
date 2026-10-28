using ExpenseTracker;
using System.Globalization;

Console.WriteLine("""
============================================================
  MyBudget Expense Tracker
============================================================
""");
int choice = 0;

while (choice != 4)
{
    Console.WriteLine();
    Console.WriteLine("1) Add an expense   2) View summary   3) Set monthly budget   4) Exit");
    Console.Write("> ");

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out choice))
    {
        Console.WriteLine("Please enter a number from 1 to 4.");
        choice = 0;
        continue;
    }

    switch (choice)
    {
        case 1:
            Console.WriteLine("Add expense — coming next.");
            break;

        case 2:
            Console.WriteLine("Summary — coming next.");
            break;

        case 3:
            Console.WriteLine("Set budget — coming next.");
            break;

        case 4:
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Choose a number from 1 to 4.");
            break;
    }
}
