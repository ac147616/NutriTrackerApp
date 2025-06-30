using Microsoft.Identity.Client;
using System;
using System.ComponentModel.Design;

namespace NutriTrackerApp
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;
        public string userType = "";
        public int? TheUserID = null;
        public int? TheAdminID = null;
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
            string prompt = "\nWelcome to the NutriTracker App, Please choose your role using the arrow keys and pressing enter to select\n";
            string[] options = { "New User", "Existing User", "Admin", "Help", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            Program myProgram = new Program();
            int SelectedIndex = mainMenu.Run();

            switch (SelectedIndex)
            {
                case 0:
                    myProgram.InsertNewUser();
                    break;
                case 1:
                    myProgram.ExistingUserLogIn();
                    //myProgram.ViewAllUsers();

                    break;
                case 2:
                    myProgram.AdminLogIn();
                    break;
                case 3:
                    myProgram.GetHelp();
                    break;
                case 4:
                    myProgram.Exit();
                    break;
            }

        }

        public void ViewAllUsers()
        {
            view.Clear();
            List<UserDetails> allUsers = storageManager.GetAllUserDetails();

            if (allUsers.Count == 0)
            {
                Console.WriteLine("No users found in the database.");
            }
            else
            {
                Console.WriteLine("List of Registered Users:\n");
                Console.WriteLine($"{"ID",-5} {"Name",-20} {"Email",-25} {"Age",-5} {"Gender",-8} {"Weight",-8} {"Height",-8}");

                foreach (var user in allUsers)
                {
                    string fullName = $"{user.FirstName} {user.LastName}";
                    Console.WriteLine($"{user.UserID,-5} {fullName,-20} {user.EmailID,-25} {user.Age,-5} {user.Gender,-8} {user.UserWeight,-8} {user.UserHeight,-8}");
                }
            }

            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey(true);
        }


        public void UserHomePage()
        {
            string prompt = "\nYou have arrived at the User Home Page, choose an option using the arrow keys and pressing enter to select\n";
            string[] options = { "Settings", "Allergies", "Food", "Diet Plans", "Goals", "Daily Log", "Help", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();
            view.Clear();

            switch (SelectedIndex)
            {
                case 0:
                    UserOptions();
                    break;
                case 1:
                    AllergiesOptions();
                    break;
                case 2:
                    FoodOptions();
                    break;
                case 3:
                    DietPlansOptions();
                    break;
                case 4:
                    GoalsOptions();
                    break;
                case 5:
                    DailyLogOptions();
                    break;
                case 6:
                    GetHelp();
                    break;
                case 7:
                    Exit();
                    break;
            }

        }
        public void AdminHomePage()
        {
            string prompt = "\nYou have arrived at the Admin Home Page, choose an option using the arrow keys and pressing enter to select\n";
            string[] options = { "Users", "Admins", "Foods", "Diet Plans", "Help", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();
            view.Clear();

            switch (SelectedIndex)
            {
                case 0:
                    UserOptions();
                    break;
                case 1:
                    AdminOptions();
                    break;
                case 2:
                    FoodOptions();
                    break;
                case 3:
                    DietPlansOptions();
                    break;
                case 4:
                    GetHelp();
                    break;
                case 5:
                    Exit();
                    break;
            }
        }
        public void InsertNewUser()
        { 
            view.Clear();
            Console.WriteLine("Checking if successfully signed up");
            System.Threading.Thread.Sleep(1000);
            userType = "user";
            UserHomePage();
        }
        
        public void ExistingUserLogIn()
        {
            while (true)
            {
                view.Clear();
                Console.WriteLine("Enter your ID and password, use CTR + ENTER to submit and use the arrows and backspace to navigate!");

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
                "User ID",
                "Password",
                });

                try
                {
                    int userID = Convert.ToInt32(collectedResponses[0]);
                    string passwordkey = collectedResponses[1];
                    userType = "user";
                    int? result = storageManager.GetUserID(userID, passwordkey);
                    if (result == null)
                    {
                        ShowMessage("Incorrect username or password, press any key to try again");
                    }
                    else
                    {
                        TheUserID = result;
                        break;
                    }
                }
                catch (Exception ex)
                
                {
                    ShowMessage("Incorrect User ID. Must be a number. Press any key to try again");
                }

            }

            UserHomePage();
           
        }

        public void AdminLogIn()
        {
            view.Clear();
            Console.WriteLine("Checking if successfully logged in");
            userType = "admin";
            System.Threading.Thread.Sleep(1000);
            AdminHomePage();
        }
        public void UserOptions()
        {
            if (userType == "user")
            {
                string prompt = "\nSettings, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View you details", "Update your details", "Delete account", "Back to home page", "Help",};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW YOUR DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 1:
                        Console.WriteLine("UPDATE YOUR DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key1 = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 2:
                        Console.WriteLine("DELETE YOUR DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key4 = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 3:
                        UserHomePage();
                        break;
                    case 4:
                        GetHelp();
                        view.Clear();
                        UserOptions();
                        break;
                }
            }
            else
            {
                string prompt = "\nManage Users, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all users", "Insert new user", "Update existing user details", "Delete a user", "Back to home page", "Help",};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All User DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 1:
                        Console.WriteLine("insert new user COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key1 = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 2:
                        Console.WriteLine("Update an existing user details COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key4 = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 3:
                        Console.WriteLine("DELETE a user COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key5 = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 4:
                        AdminHomePage();
                        break;
                    case 5:
                        GetHelp();

                        view.Clear();
                        UserOptions();
                        break;
                }
            }
        }

        public void AdminOptions()
        {
            string prompt = "\nManage Admins, choose an option using the arrow keys and pressing enter to select\n";
            string[] options = { "View all admins", "Insert new admin", "Update existing admin", "Delete an admin", "Back to home page", "Help", };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();
            view.Clear();

            switch (SelectedIndex)
            {
                case 0:
                    Console.WriteLine("VIEW All admin DETAILS COMING SOON...Press any key to go back");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    AdminOptions();
                    break;
                case 1:
                    Console.WriteLine("Insert new admin COMING SOON...Press any key to go back");
                    ConsoleKeyInfo key1 = Console.ReadKey(true);
                    AdminOptions();
                    break;
                case 2:
                    Console.WriteLine("update existing admin...Press any key to go back");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    AdminOptions();
                    break;
                case 3:
                    Console.WriteLine("delete an admin Press any key to go back");
                    ConsoleKeyInfo key3 = Console.ReadKey(true);
                    AdminOptions();
                    break;
                case 4:
                    AdminHomePage();
                    break;
                case 5:
                    GetHelp();

                    view.Clear();
                    AdminOptions();
                    break;
            }
        }
        public void AllergiesOptions()
        {
            string prompt = "\nManage your allergies, choose an option using the arrow keys and pressing enter to select\n";
            string[] options = { "View all allergies", "Insert new allergy",  "Delete an allergy", "Back to home page", "Help", };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();
            view.Clear();

            switch (SelectedIndex)
            {
                case 0:
                    Console.WriteLine("view all allergies DETAILS COMING SOON...Press any key to go back");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    AllergiesOptions();
                    break;
                case 1:
                    Console.WriteLine("Insert new allergy COMING SOON...Press any key to go back");
                    ConsoleKeyInfo key1 = Console.ReadKey(true);
                    AllergiesOptions();
                    break;
                case 2:
                    Console.WriteLine("delete an allergy Press any key to go back");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    AllergiesOptions();
                    break;
                case 3:
                    UserHomePage();
                    break;
                case 4:
                    GetHelp();

                    view.Clear();
                    AllergiesOptions();
                    break;
            }
        }

        public void FoodOptions()
        {
            if (userType == "user")
            {
                string prompt = "\nView Foods, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all foods", "Back to home page", "Help", };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All foods DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        FoodOptions();
                        break;
                    case 4:
                        UserHomePage();
                        break;
                    case 5:
                        GetHelp();

                        view.Clear();
                        FoodOptions();
                        break;
                }
            }
            else
            {
                string prompt = "\nManage Foods, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all foods", "Insert new food", "Update existing food", "Delete a food", "Back to home page", "Help", };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All food DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        FoodOptions();
                        break;
                    case 1:
                        Console.WriteLine("Insert new food COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key1 = Console.ReadKey(true);
                        FoodOptions();
                        break;
                    case 2:
                        Console.WriteLine("update existing food...Press any key to go back");
                        ConsoleKeyInfo key2 = Console.ReadKey(true);
                        FoodOptions();
                        break;
                    case 3:
                        Console.WriteLine("delete a food Press any key to go back");
                        ConsoleKeyInfo key3 = Console.ReadKey(true);
                        FoodOptions();
                        break;
                    case 4:
                        AdminHomePage();
                        break;
                    case 5:
                        GetHelp();

                        view.Clear();
                        FoodOptions();
                        break;
                }
            }
        }

        public void DietPlansOptions()
        {
            if (userType == "user")
            {
                string prompt = "\nView Diet Plans, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all diet plans", "Back to home page", "Help", };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All diet plans DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                    case 4:
                        UserHomePage();
                        break;
                    case 5:
                        GetHelp();

                        view.Clear();
                        DietPlansOptions();
                        break;
                }
            }
            else
            {
                string prompt = "\nManage Diet Plans, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all diet plans", "Insert new diet plan", "Update existing diet plan", "Delete a diet plan", "Back to home page", "Help", };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run();
                view.Clear();

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All diet plans DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                    case 1:
                        Console.WriteLine("Insert new diet plan COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key1 = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                    case 2:
                        Console.WriteLine("update existing diet plan...Press any key to go back");
                        ConsoleKeyInfo key2 = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                    case 3:
                        Console.WriteLine("delete a diet plan Press any key to go back");
                        ConsoleKeyInfo key3 = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                    case 4:
                        AdminHomePage();
                        break;
                    case 5:
                        GetHelp();

                        view.Clear();
                        DietPlansOptions();
                        break;
                }
            }
        }

        public void GoalsOptions()
        {

        }

        public void DailyLogOptions()
        {

        }

        public void Exit()
        {
            view.Clear();
            Menu mainMenu = new Menu("\nAre you sure you want to exit the application?\n", ["yes", "no"]);
            int SelectedIndex = mainMenu.Run();

            switch (SelectedIndex)
            {
                case 0:
                    view.Clear();
                    Console.WriteLine("Bye Bye");
                    System.Environment.Exit(0);
                    break;
                case 1:
                    view.Clear();
                    
                    if (userType == "")
                    {
                        RunUserTypeMenu();
                    }
                    else if (userType == "user")
                    {
                        UserHomePage();
                    }
                    else
                    {
                        AdminHomePage();
                    }
                        break;
            }
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

        public void ShowMessage(string message)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey(true);
        }
        public void GetHelp()
        {
            view.Help();
            Console.WriteLine("\nPress any key to go back...");
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (userType == "")
            {
                RunUserTypeMenu();
            }
            else if (userType == "user")
            {
                UserHomePage();
            }
            else
            {
                AdminHomePage();
            }
        }
    }
}
