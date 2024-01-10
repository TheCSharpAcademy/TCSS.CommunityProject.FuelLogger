using Spectre.Console;
using TCSS.CommunityProject.FuelLogger.Models;

namespace TCSS.CommunityProject.FuelLogger;

internal class UserInterface
{
    internal static void MainMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            var menuOptions = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("[underline]Fuel Logger[/]")
                .AddChoices(
                    MenuOptions.AddVehicle,
                    MenuOptions.ViewVehicle,
                    MenuOptions.UpdateVehicle,
                    MenuOptions.DeleteVehicle,
                    MenuOptions.CloseApplication)
                );


            switch (menuOptions)
            {
                case MenuOptions.AddVehicle:
                    AddVehicle();
                    break;
            }
        }
    }

    internal static void AddVehicle()
    {
        Console.Clear();

        string dateCreated = DateTime.Now.ToString();
        string Make = AnsiConsole.Ask<string>("Enter the Make of vehicle: ");
        string Model = AnsiConsole.Ask<string>("Enter the Model of vehicle: ");

        int Fuel = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter the Fuel type (1 for Oil, 2 for Diesel): ")
            .ValidationErrorMessage("[red]That's not a valid fuel type[/]")
            .Validate(fuel =>
            {
            return fuel switch
            {
                <= 0 => ValidationResult.Error("[red]Enter 1 or 2[/]"),
                > 2 => ValidationResult.Error("[red]Enter 1 or 2[/]"),
                _ => ValidationResult.Success(),
            };
        }));
        bool valid=true;
        int Year=0;
        do
        {
            Year = AnsiConsole.Ask<int>("Enter the model Year of vehicle (yyyy): ");
            string strYear = Year.ToString();
            if (strYear.Length != 4 || strYear.StartsWith("0"))
            {
                valid = false;
            }
            else
                valid = true;
        } while (!valid);
        Vehicle vehicle = new Vehicle();
        vehicle.Year = Year;
        vehicle.Model = Model;
        vehicle.Make = Make;
        vehicle.FuelType = Fuel;
        vehicle.DateCreated = dateCreated;
        DataAccess dataAccess = new();
        dataAccess.AddVehicle(vehicle);
        Console.WriteLine("Vehicle recorded. Press Enter to continue...");

    }
}
