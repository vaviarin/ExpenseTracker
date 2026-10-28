using ExpenseTracker;
using System.Globalization;

Console.WriteLine("""
============================================================
  MyBudget Expense Tracker
============================================================
""");
int choice = 0;
decimal totalSpent = 0m;
decimal monthlyBudget = 0m;
bool budgetSet = false;

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
        case 3:
            {
                bool validBudget = false;

                while (!validBudget)
                {
                    Console.Write("Monthly budget: ");
                    string? budgetInput = Console.ReadLine();

                    if (!decimal.TryParse(budgetInput, out decimal enteredBudget))
                    {
                        Console.WriteLine("Please enter a valid number.");
                        continue;
                    }

                    try
                    {
                        monthlyBudget = BudgetRules.ValidateAmount(enteredBudget);
                        budgetSet = true;
                        validBudget = true;

                        decimal remaining = monthlyBudget - totalSpent;

                        Console.WriteLine(
                            $"Budget set to {BudgetRules.FormatCurrency(monthlyBudget)}.");
                        Console.WriteLine(
                            $"Remaining: {BudgetRules.FormatCurrency(remaining)} — " +
                            $"{BudgetRules.BudgetStatus(remaining, monthlyBudget)}");
                    }
                    catch (InvalidExpenseException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                break;
            }

        case 4:
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Choose a number from 1 to 4.");
            break;
    }
}
