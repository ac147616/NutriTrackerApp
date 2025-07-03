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
        public int TheID = 0;
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=nutriTracker2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            System.Threading.Thread.Sleep(2500);
          
            view = new ConsoleView();
            view.Clear("Welcome!");

            RunUserTypeMenu(); //This menu shows the options for the type of user


            storageManager.CloseConnection();
        }
        public static void RunUserTypeMenu()
        {
            view.Clear("Welcome!");
            string prompt = "";
            string[] options = { "New User", "Existing User", "Admin"};
            Menu mainMenu = new Menu(prompt, options);
            Program myProgram = new Program();
            int SelectedIndex = mainMenu.Run("Welcome!");

            switch (SelectedIndex)
            {
                case 0:
                    myProgram.InsertNewUser();
                    break;
                case 1:
                    myProgram.ExistingUserLogIn();
                    break;
                case 2:
                    myProgram.AdminLogIn();
                    break;
            }

        }
        public void UserHomePage()
        {
            string prompt = "";
            string[] options = { "Settings", "Allergies", "Food", "Diet Plans", "Goals", "Daily Log"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("User Home Page");
            view.Clear("User Home Page");

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
            }

        }
        public void AdminHomePage()
        {
            string prompt = "";
            string[] options = { "Users", "Admins", "Foods", "Diet Plans"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("Admin Home Page");
            view.Clear("Admin Hone Page");

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
            }
        }
        public void InsertNewUser()
        {
            while (true)
            {
                view.Clear("Sign Up");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
                "First Name",
                "Last Name",
                "Email Adress",
                "Password",
                "Confirm Password",
                "Age",
                "Gender (e.g female or male)",
                "Weight e.g 45",
                "Height e.g 172",
                });

                if (collectedResponses[0] == "" || collectedResponses[1] == "" || collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("Required fields cannot be empty, press any key to fill again", 9);
                }
                else if (collectedResponses[3] != collectedResponses[4])
                {
                    ShowMessage("Password does not match confirmed password, press any key to fill again", 9);
                }
                else
                {
                    try
                    {
                        string firstName = collectedResponses[0];
                        string lastname = collectedResponses[1];
                        string emailID = collectedResponses[2];
                        string passwordkey = collectedResponses[3];
                        int age = Convert.ToInt32(collectedResponses[5]);
                        string gender = collectedResponses[6];
                        double weight = Convert.ToDouble(collectedResponses[7]);
                        double height = Convert.ToDouble(collectedResponses[8]);
                        string date = DateTime.Now.ToString();
                        int userID = 0;
                        UserDetails user1 = new UserDetails(userID, firstName, lastname, emailID, passwordkey, age, gender, weight, height, date);
                        TheID = storageManager.InsertUser(user1);
                        userType = "user";
                        ShowMessage($"New user created with ID: {TheID}, press any key to continue", 9);
                        break;
                    }
                    catch (Exception ex)
                    {

                        ShowMessage("Ivalid format (age, weight, height must be integer and gender can only be male, female), press any key to fill again", 9);
                    }
                }
            }
            if (userType == "admin")
            {
                UserOptions();
            }
            else
            {
                UserHomePage();
            }
        }
        public void InsertNewAdmin()
        {
            while (true)
            {
                view.Clear("Create New Admin");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "First Name",
            "Last Name",
            "Email Address",
            "Password",
            "Confirm Password"
                });

                // Basic validation
                if (collectedResponses[0] == "" || collectedResponses[1] == "" ||
                    collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 9);
                }
                else if (collectedResponses[3] != collectedResponses[4])
                {
                    ShowMessage("Password and Confirm Password do not match. Press any key to fill again.", 9);
                }
                else
                {
                    try
                    {
                        string firstName = collectedResponses[0];
                        string lastName = collectedResponses[1];
                        string email = collectedResponses[2];
                        string password = collectedResponses[3];

                        AdminDetails newAdmin = new AdminDetails(0, firstName, lastName, email, password);
                        int adminID = storageManager.InsertAdmin(newAdmin);

                        ShowMessage($"New admin created with ID: {adminID}, press any key to continue", 9);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("An error occurred while creating admin. Press any key to try again.", 9);
                    }
                }
            }

            AdminOptions();
        }
        public void ExistingUserLogIn()
        {
            while (true)
            {
                view.Clear("User Log In");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

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
                    int result = storageManager.GetUserID(userID, passwordkey);
                    if (result == 0)
                    {
                        ShowMessage("Invalid ID and password combination, press any key to try again", 2);
                    }
                    else
                    {
                        TheID = result;
                        break;
                    }
                }
                catch (Exception ex)
                
                {
                    ShowMessage("ID must be a number, press any key to try again", 2);
                }

            }

            UserHomePage();
           
        }
        public void AdminLogIn()
        {
            while (true)
            {
                view.Clear("Admin Log In");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
                "Admin ID",
                "Password",
                });

                try
                {
                    int adminID = Convert.ToInt32(collectedResponses[0]);
                    string passwordkey = collectedResponses[1];
                    userType = "admin";
                    int result = storageManager.GetAdminID(adminID, passwordkey);
                    if (result == 0)
                    {
                        ShowMessage("Invalid ID and password combination, press any key to try again", 2);
                    }
                    else
                    {
                        TheID = result;
                        break;
                    }
                }
                catch (Exception ex)

                {
                    ShowMessage("ID must be a number, press any key to try again", 2);
                }

            }

            AdminHomePage();
        }
        public void UpdateUser(int TheID)
        {
            ConsoleView view = new ConsoleView();
            UserDetails user = storageManager.GetUserByID(TheID);

            while (true)
            {
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                }

                view.Clear("Update User");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "First Name",
            "Last Name",
            "Email Address",
            "Password",
            "Confirm Password",
            "Age",
            "Gender (female or male)",
            "Weight (e.g 45)",
            "Height (e.g 172)"
                });

                if (collectedResponses[0] == "" || collectedResponses[1] == "" || collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("Required fields cannot be empty, press any key to fill again", 9);
                }
                else if (collectedResponses[3] != collectedResponses[4])
                {
                    ShowMessage("Password does not match confirmed password, press any key to fill again", 9);
                }
                else
                {
                    try
                    {
                        user.FirstName = collectedResponses[0];
                        user.LastName = collectedResponses[1];
                        user.EmailID = collectedResponses[2];
                        user.Passwordkey = collectedResponses[3];
                        user.Age = Convert.ToInt32(collectedResponses[5]);
                        user.Gender = collectedResponses[6];
                        user.UserWeight = Convert.ToDouble(collectedResponses[7]);
                        user.UserHeight = Convert.ToDouble(collectedResponses[8]);

                        storageManager.UpdateUser(user);

                        ShowMessage("User updated successfully. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Please try again.", 9);
                    }
                }
            }

            UserOptions();
        }
        public void UpdateAdmin(int TheID)
        {
            AdminDetails admin = storageManager.GetAdminByID(TheID);

            while (true)
            {
                if (admin == null)
                {
                    Console.WriteLine("Admin not found.");
                    return;
                }

                view.Clear("Update Admin");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "First Name",
            "Last Name",
            "Email Address",
            "Password",
            "Confirm Password"
                });

                if (collectedResponses[0] == "" || collectedResponses[1] == "" || collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("Required fields cannot be empty, press any key to fill again", 9);
                }
                else if (collectedResponses[3] != collectedResponses[4])
                {
                    ShowMessage("Password does not match confirmed password, press any key to fill again", 9);
                }
                else
                {
                    try
                    {
                        admin.FirstName = collectedResponses[0];
                        admin.LastName = collectedResponses[1];
                        admin.EmailID = collectedResponses[2];
                        admin.Passwordkey = collectedResponses[3];

                        storageManager.UpdateAdmin(admin);

                        ShowMessage("Admin updated successfully. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Please try again.", 9);
                    }
                }
            }

            AdminOptions();
        }
        public void DeleteUser(int userID)
        {
            Menu confirmDelete;
            
            view.Clear("Delete User");
            if (userType == "admin")
            {
                confirmDelete = new Menu("\nAre you sure you want to delete this account?\n", new string[] { "Yes", "No" });
            }
            else
            {
                confirmDelete = new Menu("\nAre you sure you want to delete your account?\n", new string[] { "Yes", "No" });
            }
            
            int selectedIndex = confirmDelete.Run("Delete Account");

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteUserByID(userID);
                    view.Clear("Delete User");
                    if (success)
                    {
                        Console.WriteLine("This account has been successfully deleted.");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("An error occurred. The account could not be deleted. It may be it doesnt exist");
                    }
                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey(true);
                    UserOptions();
                    break;

                case 1:
                    UserOptions();
                    break;
            }
        }
        public void DeleteAdmin(int adminID)
        {
            view.Clear("Delete Admin");
            Menu confirmDelete = new Menu("\nAre you sure you want to delete this admin account?\n", new string[] { "Yes", "No" });

            int selectedIndex = confirmDelete.Run("Delete Admin");

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteAdminByID(adminID);
                    view.Clear("Delete Admin");

                    if (success)
                    {
                        Console.WriteLine("The admin account has been successfully deleted.");
                        if (adminID == TheID)
                        {
                            System.Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("An error occurred. The account could not be deleted. It may not exist.");
                    }

                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey(true);
                    AdminOptions();
                    break;

                case 1:
                    AdminOptions();
                    break;
            }
        }
        public void UserOptions()
        {
            if (userType == "user")
            {
                string prompt = "";
                string[] options = { "View you details", "Update your details", "Delete account" };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("Settings");
                view.Clear("Settings");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.PrintUserDetails(userType, TheID);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nPress any key to go back");
                        Console.ResetColor();
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 1:
                        UpdateUser(TheID);
                        break;
                    case 2:
                        DeleteUser(TheID);
                        break;
                }
            }
            else
            {
                string prompt = "";
                string[] options = { "View all users", "Insert new user", "Update existing user details", "Delete a user" };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("Manage Users");
                view.Clear("Manage Users");

                switch (SelectedIndex)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Can only show top 100 users");
                        Console.ResetColor();
                        storageManager.PrintUserDetails(userType, TheID);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nPress any key to go back");
                        Console.ResetColor();
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        UserOptions();
                        break;
                    case 1:
                        InsertNewUser();
                        break;
                    case 2:
                        view.Clear("Edit User Details");
                        Console.Write("\nUserID to edit: ");
                        try
                        {
                            int usersID = Convert.ToInt32(Console.ReadLine());
                            UpdateUser(usersID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            UserOptions();
                        }
                        break;
                    case 3:
                        view.Clear("Delete User");
                        Console.Write("\nUserID to delete: ");
                        try
                        {
                            int usersID = Convert.ToInt32(Console.ReadLine());
                            DeleteUser(usersID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            UserOptions();
                        }
                        break;
                }
            }
        }
        public void AdminOptions()
        {
            string prompt = "";
            string[] options = { "View all admins", "Insert new admin", "Update existing admin", "Delete an admin"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("Manage Admins");
            view.Clear("Manage Admins");

            switch (SelectedIndex)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Can only show a 100 results");
                    Console.ResetColor();
                    storageManager.PrintAdminDetails();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nPress any key to go back");
                    Console.ResetColor();
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    UserOptions();
                    break;
                case 1:
                    InsertNewAdmin();
                    break;
                case 2:
                    view.Clear("Edit User Details");
                    Console.Write("\nAdminID to edit: ");
                    try
                    {
                        int usersID = Convert.ToInt32(Console.ReadLine());
                        UpdateAdmin(usersID);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                        Console.ReadKey(true);
                        AdminOptions();
                    }
                    break;
                case 3:
                    view.Clear("Delete Admin");
                    Console.Write("\nAdminID to delete: ");
                    try
                    {
                        int adminsID = Convert.ToInt32(Console.ReadLine());
                        DeleteAdmin(adminsID);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                        Console.ReadKey(true);
                        UserOptions();
                    }
                    break;
            }
        }
        public void AllergiesOptions()
        {
            string prompt = "";
            string[] options = { "View all allergies", "Insert new allergy",  "Delete an allergy"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("");
            view.Clear("");

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

                    view.Clear("");
                    AllergiesOptions();
                    break;
            }
        }
        public void FoodOptions()
        {
            if (userType == "user")
            {
                string prompt = "\nView Foods, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all foods"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("");
                view.Clear("");

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All foods DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        FoodOptions();
                        break;
                }
            }
            else
            {
                string prompt = "\nManage Foods, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all foods", "Insert new food", "Update existing food", "Delete a food"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("");
                view.Clear("");

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

                        view.Clear("");
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
                string[] options = { "View all diet plans"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("");
                view.Clear("");

                switch (SelectedIndex)
                {
                    case 0:
                        Console.WriteLine("VIEW All diet plans DETAILS COMING SOON...Press any key to go back");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        DietPlansOptions();
                        break;
                }
            }
            else
            {
                string prompt = "\nManage Diet Plans, choose an option using the arrow keys and pressing enter to select\n";
                string[] options = { "View all diet plans", "Insert new diet plan", "Update existing diet plan", "Delete a diet plan"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("");
                view.Clear("");

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

                        view.Clear("");
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
            view.Clear("");
            Menu mainMenu = new Menu("\nAre you sure you want to exit the application?\n", ["yes", "no"]);
            int SelectedIndex = mainMenu.Run("");

            switch (SelectedIndex)
            {
                case 0:
                    view.Clear("");
                    Console.WriteLine("Bye Bye");
                    System.Environment.Exit(0);
                    break;
                case 1:
                    view.Clear("");
                    
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
        public void ShowMessage(string message, int labelsLength)
        {
            if (message.Length < 100)
            {
                int spaceLeftForHeader = 15;
                int messagePosY = spaceLeftForHeader + (labelsLength * 2);
                Console.SetCursorPosition(50, messagePosY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
                Console.ReadKey(true);
            }
            else
            {
                int spaceLeftForHeader = 15;
                int messagePosY = spaceLeftForHeader + (labelsLength * 2);
                Console.SetCursorPosition(0, messagePosY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
                Console.ReadKey(true);
            }
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
