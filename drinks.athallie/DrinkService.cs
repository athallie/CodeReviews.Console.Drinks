using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks.athallie
{
    internal class DrinkService
    {
        private ApiClient ApiClient;
        private DrinksWrapper<DrinksCategory> DrinksCategories;
        private DrinksWrapper<Drinks> DrinksOfChosenCategories;

        public DrinkService(ApiClient apiClient) { 
            ApiClient = apiClient;
            UpdateDrinksCategories();
        }

        private async Task UpdateDrinksCategories()
        {
           DrinksCategories =  await ApiClient
                .ProcessRepositoryAsync<DrinksWrapper<DrinksCategory>>(
               "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list"
            );
        }

        private async Task<DrinksWrapper<Drinks>> GetDrinksOfCategory(string category)
        {
            return await ApiClient
                .ProcessRepositoryAsync<DrinksWrapper<Drinks>>(
                $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category.Replace(" ", "_")}"
            );
        }

        public async Task<bool> ShowMenuCategories()
        {
            await UpdateDrinksCategories();
            if (DrinksCategories != null) {
                Console.WriteLine("".PadLeft(30, '-'));
                Console.WriteLine(
                    "|".PadRight(2) +
                    "Categories Menu" +
                    "|".PadLeft(28 - "Categories Menu".Length)
                );

                foreach (var category in DrinksCategories.Drinks)
                {
                    Console.WriteLine("".PadLeft(30, '-'));
                    Console.WriteLine(
                        "|".PadRight(2) +
                        category.Category +
                        "|".PadLeft(28 - category.Category.Length)
                    );
                }
                Console.WriteLine("".PadLeft(30, '-'));

                Console.WriteLine("\n*Type \"quit\" to exit*");

                return true;
            } else
            {
                Console.WriteLine("Sorry, there's some trouble on our end. Please wait a moment.");
                return false;
            }
        }

        public string AskForCategory()
        {
            string chosenCategory;
            while (true)
            {
                Console.Write("\nChoose Category: ");
                chosenCategory = Console.ReadLine();
                Console.WriteLine();
                string formattedCategoryStr = $"{chosenCategory[0]}".ToUpper() + chosenCategory.Trim().ToLower().Substring(1);
                if (DrinksCategories.Drinks.Contains(new DrinksCategory(formattedCategoryStr)))
                {
                    return chosenCategory;
                } else if (chosenCategory.ToLower() == "quit")
                {
                    Console.WriteLine("Thank you for coming.");
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine(
                        "There's no such category. " +
                        "Please input a category based on the list above."
                    );
                }
                Console.WriteLine();
            }
        }

        public async Task ShowDrinksOfCategory(string category)
        {
            var drinks = await GetDrinksOfCategory(category);
            if (drinks != null) {
                string formattedCategoryStr = $"{category[0]}".ToUpper() + category.Trim().ToLower().Substring(1);
                Console.WriteLine("".PadLeft(100, '-'));
                Console.WriteLine(
                    "|".PadRight(2) +
                    formattedCategoryStr +
                    "|".PadLeft(98 - formattedCategoryStr.Length)
                );
                foreach (var drink in drinks.Drinks)
                {
                    Console.WriteLine("".PadLeft(100, '-'));
                    Console.Write("|".PadRight(2));
                    Console.Write(drink.Name);
                    Console.WriteLine("|".PadLeft(98 - drink.Name.Length));
                }
                Console.WriteLine("".PadLeft(100, '-'));
            }
            else
            {
                Console.WriteLine("You haven't chosen a category. Please choose one that's on the menu.");
            }   
        }
    }
}
