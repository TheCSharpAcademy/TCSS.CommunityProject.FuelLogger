using Spectre.Console;

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
        }
    }
}
