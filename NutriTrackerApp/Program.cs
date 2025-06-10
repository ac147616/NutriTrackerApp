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

            RunMainMenu(); //This menu shows the options for the type of user


            storageManager.CloseConnection();
        }

        public static void RunMainMenu()
        {
            string prompt = @"
███╗   ██╗██╗   ██╗████████╗██████╗ ██╗████████╗██╗ ██████╗ ███╗   ██╗    ████████╗██████╗  █████╗  ██████╗██╗  ██╗███████╗██████╗ 
████╗  ██║██║   ██║╚══██╔══╝██╔══██╗██║╚══██╔══╝██║██╔═══██╗████╗  ██║    ╚══██╔══╝██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗
██╔██╗ ██║██║   ██║   ██║   ██████╔╝██║   ██║   ██║██║   ██║██╔██╗ ██║       ██║   ██████╔╝███████║██║     █████╔╝ █████╗  ██████╔╝
██║╚██╗██║██║   ██║   ██║   ██╔══██╗██║   ██║   ██║██║   ██║██║╚██╗██║       ██║   ██╔══██╗██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗
██║ ╚████║╚██████╔╝   ██║   ██║  ██║██║   ██║   ██║╚██████╔╝██║ ╚████║       ██║   ██║  ██║██║  ██║╚██████╗██║  ██╗███████╗██║  ██║
╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝       ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                                                                                                                                   


Welcome to the NutriTracker App, Please choose your role using the arrow keys and pressing enter to select";
            string[] options = { "New User", "Existing User", "Admin", "Help", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();

            switch (SelectedIndex)
            {
                case 0:
                    InsertNewUser();
                    break;
                case 1:
                    ExistingUserLogIn();
                    break;
                case 2:
                    AdminLogIn();
                    break;
                case 3:
                    GetHelp();
                    break;
                case 4:
                    Exit();
                    break;
            }

        }

        public static void InsertNewUser()
        { 
            Console.Clear();
            Console.WriteLine("new user");
        }
        public static void ExistingUserLogIn()
        {
            Console.Clear();
            Console.WriteLine("user");
        }

        public static void AdminLogIn()
        {
            Console.Clear();
            Console.WriteLine("admin");
        }
        
        public static void GetHelp()
        {
            ConsoleView.Help();
        }
        public static void Exit()
        {
            Console.Clear();
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
