using System;

namespace NutriTrackerApp
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=nutriTracker2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            System.Threading.Thread.Sleep(2500);
            Console.Clear();
            view = new ConsoleView();
            string choice = view.DisplayMenu();


            switch(choice)
            {
                case "1":
                    {
                        List<Food> foods = storageManager.GetAllFoods();
                        view.DisplayFoods(foods);
                    }
                    break;
                case "2":
                    {
                        UpdateFoodName();
                        break;
                    }
                case "3":
                    {
                        InsertNewFood();
                        break;
                    }
                case "4":
                    {
                        DeleteFoodByName();
                        break;
                    }
                case "5":
                    {
                        //End Loop
                        break;
                    }
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            storageManager.CloseConnection();
        }

        private static void UpdateFoodName()
        {
            view.DisplayMessage("Enter the food_id to update: ");
            int foodID = view.GetIntInput();
            view.DisplayMessage("Enter the new food name: ");
            string foodName = view.GetInput();
            int rowsAffected = storageManager.UpdateFoodName(foodID, foodName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }
        private static void InsertNewFood()
        {
            view.DisplayMessage("Enter the new food name: ");
            string foodName = view.GetInput();
            string category = view.GetInput();
            double calories = Convert.ToDouble(view.GetInput());
            double carbohydrates = Convert.ToDouble(view.GetInput());
            double proteins = Convert.ToDouble(view.GetInput());
            double fats = Convert.ToDouble(view.GetInput());
            double servingSize = Convert.ToDouble(view.GetInput());
            int foodID = 0;
            Food food1 = new Food(foodID, foodName, category, calories, carbohydrates, proteins, fats, servingSize);
            int generatedId = storageManager.InsertFood(food1);
            view.DisplayMessage($"New food inserted with ID: {generatedId}");

        }

        private static void DeleteFoodByName()
        {
            view.DisplayMessage("Enter the food name to delete: ");
            string foodName = view.GetInput();
            int rowsAffected = storageManager.DeleteFoodByName(foodName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }
    }
}
