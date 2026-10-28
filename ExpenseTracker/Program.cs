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
            {
                string? description = null;

                while (string.IsNullOrWhiteSpace(description))
                {
                    Console.Write("Description: ");
                    description = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(description))
                    {
                        Console.WriteLine("Description cannot be empty.");
                    }
                }

                description = description.Trim();
                decimal amount = 0m;
                bool validAmount = false;

                while (!validAmount)
                {
                    Console.Write("Amount: ");
                    string? amountInput = Console.ReadLine();

                    if (!decimal.TryParse(amountInput, out decimal enteredAmount))
                    {
                        Console.WriteLine("Please enter a number.");
                        continue;
                    }

                    try
                    {
                        amount = BudgetRules.ValidateAmount(enteredAmount);
                        validAmount = true;
                    }
                    catch (InvalidExpenseException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                string? category = null;

                while (category == null)
                {
                    Console.Write("Category [Food/Transport/Utilities/Entertainment/Other]: ");
                    category = BudgetRules.NormalizeCategory(Console.ReadLine());

                    if (category == null)
                    {
                        Console.WriteLine("Unknown category. Please try again.");
                    }
                }
                DateOnly expenseDate = DateOnly.FromDateTime(DateTime.Today);
                bool validDate = false;

                while (!validDate)
                {
                    Console.Write("Date (yyyy-mm-dd, blank = today): ");
                    string? dateInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(dateInput))
                    {
                        expenseDate = DateOnly.FromDateTime(DateTime.Today);
                        validDate = true;
                    }
                    else if (!DateOnly.TryParseExact(
                                 dateInput,
                                 "yyyy-MM-dd",
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.None,
                                 out expenseDate))
                    {
                        Console.WriteLine("Use the format yyyy-mm-dd.");
                    }
                    else if (expenseDate > DateOnly.FromDateTime(DateTime.Today))
                    {
                        Console.WriteLine("Date cannot be in the future.");
                    }
                    else
                    {
                        validDate = true;
                    }
                }

                Console.Write("Note (optional): ");
                string? noteInput = Console.ReadLine();

                string note = noteInput?.Trim() ?? string.Empty;
                string noteText = note.Length == 0 ? "" : $" | Note: {note}";

                totalSpent += amount;
                Console.WriteLine(
                  $"Recorded: {description} | {BudgetRules.FormatCurrency(amount)} | " +
                  $"{category} | {expenseDate:yyyy-MM-dd} | Size: " +
                  $"{BudgetRules.ClassifyAmount(amount)}{noteText}");

                break;
            }

        case 2:
            Console.WriteLine("Summary — coming next.");
            break;

       
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
