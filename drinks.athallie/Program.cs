using System.Net.Http.Headers;
using System.Text.Json;
using drinks.athallie;

//Clients Setup
using HttpClient client = new HttpClient();
ApiClient apiClient = new ApiClient(client);
apiClient.setHeader("application/json", "Drinks App");
DrinkService drinkService = new DrinkService(apiClient);


while (true)
{
    bool menuIsShowed = await drinkService.ShowMenuCategories();
    if (menuIsShowed == false)
    {
        continue;
    }
    else
    {
        string chosenDrink = drinkService.AskForCategory();
        await drinkService.ShowDrinksOfCategory(chosenDrink);

        Console.WriteLine(
            "\nType:\n" +
            "".PadLeft(50, '-') +
            "\n|  \"back\" to see the categories list again." +
            "\n|  \"quit\" to quit the program.\n" +
            "".PadLeft(50, '-')
        );
        Console.Write("\n> ");
        string nextAction = Console.ReadLine();

        if (nextAction.ToLower() == "back")
        {
            continue;
        } else if (nextAction.ToLower() == "quit")
        {
            System.Environment.Exit(0);
        }
    }
}
