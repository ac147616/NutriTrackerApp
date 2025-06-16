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
          
            view = new ConsoleView();
            view.Clear();

            RunUserTypeMenu(); //This menu shows the options for the type of user


            storageManager.CloseConnection();
        }

        public static void RunUserTypeMenu()
        {
            Console.SetWindowSize(Math.Min(150, Console.LargestWindowWidth), Math.Min(30, Console.LargestWindowHeight));
            string prompt = "Welcome to the NutriTracker App, Please choose your role using the arrow keys and pressing enter to select";
            string[] options = { "New User", "Existing User", "Admin", "Help", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();

            switch (SelectedIndex)
            {
                case 0:
                    InsertNewUser();
                    break;
                case 1:
                    //need to put a loop here that keeps going back to last page or this page if they fail so..
                    ExistingUserLogIn();
                    //if this works...useing try catch...call the next menu page here. store the chosen variable
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
            view.Clear();
        }
        
        public static void ExistingUserLogIn()
        {
            view.Clear();
            List<string> collectedResponses = InputManager.GetInput(new string[]
            {
                "User ID",
                "Password",
            });

            
            //check if it exists
        }

        public static void AdminLogIn()
        {
            view.Clear();
            Console.WriteLine("admin");
        }
        
        public static void GetHelp()
        {
            ConsoleView.Help();
            //if they are logged in show the option to return to the home page
            //if they aren't show the option to go to usertype page
            //always show option to exit
        }
        public static void Exit()
        {
            view.Clear();
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
