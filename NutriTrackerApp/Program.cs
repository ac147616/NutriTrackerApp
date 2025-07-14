using Microsoft.Identity.Client;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection.Metadata;
using static StorageManager;

namespace NutriTrackerApp
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;
        public string userType = " ";
        public int TheID = 0;

        //application entry point – establishes database connection, initializes UI, and launches the user type selection menu
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=nutriTracker2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            System.Threading.Thread.Sleep(2500);
            view = new ConsoleView();
            view.Clear("Welcome!");

            RunUserTypeMenu(); //This menu shows the options for the type of user
        }
        //displays the main menu for selecting user type (New User, Existing User, or Admin) and goes back to eeither login or registration process
        public static void RunUserTypeMenu()
        {
            view.Clear("Welcome!");
            string prompt = "";
            string[] options = { "New User", "Existing User", "Admin"};
            Menu mainMenu = new Menu(prompt, options);
            Program myProgram = new Program();
            int SelectedIndex = mainMenu.Run("Welcome!","", myProgram);

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
        //duplicate method of the one above...reason is explained in documentation of errors.
        public void RunUserTypeMenu2()
        {
            view.Clear("Welcome!");
            string prompt = "";
            string[] options = { "New User", "Existing User", "Admin" };
            Menu mainMenu = new Menu(prompt, options);
            Program myProgram = new Program();
            int SelectedIndex = mainMenu.Run("Welcome!", "", this);

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
        //displays the user home page menu with options for managing settings, allergies, food, diet plans, goals, logs, and reports, and geos back to the feature.
        public void UserHomePage()
        {
            string prompt = "";
            string[] options = { "Settings", "Allergies", "Food", "Diet Plans", "Goals", "Daily Log", "Reports"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("User Home Page", userType, this);
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
                case 6:
                    QueryOptions();
                    break;
            }

        }
        //displays the admin home page menu with options to manage users, admins, foods, diet plans, and generate reports, then goes back to the selected option.
        public void AdminHomePage()
        {
            string prompt = "";
            string[] options = { "Users", "Admins", "Foods", "Diet Plans", "Reports"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("Admin Home Page", userType, this);
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
                case 4:
                    QueryOptions();
                    break;
            }
        }
        //handles the sign-up process for a new user including input collection, validation, user creation, and foes back to home page.
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
                }, this);

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

                        ShowMessage("Invalid format (age, weight, height must be integer and gender can only be male, female), press any key to fill again", 9);
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
        //handles the creation of a new admin account by collecting and validating input, inserting the record, and displaying the result
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
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "" ||
                    collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 6);
                }
                else if (collectedResponses[3] != collectedResponses[4])
                {
                    ShowMessage("Password and Confirm Password do not match. Press any key to fill again.", 6);
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

                        ShowMessage($"New admin created with ID: {adminID}, press any key to continue", 6);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("An error occurred while creating admin. Press any key to try again.", 6);
                    }
                }
            }

            AdminOptions();
        }
        //handles the process of adding a new allergy for the current user by collecting input, validating it, and inserting the record into the database
        public void InsertNewAllergy()
        {
            while (true)
            {
                view.Clear("Add Allergy");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ForegroundColor = ConsoleColor.White;

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Allergy (e.g. Peanuts, Dairy)"
                }, this);

                if (string.IsNullOrWhiteSpace(collectedResponses[0]))
                {
                    ShowMessage("Allergy field cannot be empty. Press any key to fill again.", 2);
                }
                else
                {
                    try
                    {
                        string allergy = collectedResponses[0];
                        bool success = storageManager.InsertAllergy(TheID, allergy);

                        if (success)
                        {
                            ShowMessage("Allergy added successfully. Press any key to continue.", 2);
                            break;
                        }
                        else
                        {
                            ShowMessage("An error occurred while adding allergy. Press any key to try again.", 2);
                        }
                    }
                    catch
                    {
                        ShowMessage("Something went wrong. Please try again.", 9);
                    }
                }
            }

            AllergiesOptions();
        }
        //handles the process of adding a new food item by collecting user input, validating it, converting nutrition fields to decimal, and inserting the data into the database. Displays success or error messages based on input and the outcome.
        public void InsertNewFood()
        {
            while (true)
            {
                view.Clear("Add New Food");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Food Name",
            "Category",
            "Calories per serving",
            "Carbohydrates (g)",
            "Proteins (g)",
            "Fats (g)",
            "Serving Size (g/mL)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "" ||
                    collectedResponses[2] == "" || collectedResponses[3] == "" ||
                    collectedResponses[4] == "" || collectedResponses[5] == "" || collectedResponses[6] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 9);
                }
                else
                {
                    try
                    {
                        string name = collectedResponses[0];
                        string category = collectedResponses[1];
                        decimal calories = Convert.ToDecimal(collectedResponses[2]);
                        decimal carbs = Convert.ToDecimal(collectedResponses[3]);
                        decimal proteins = Convert.ToDecimal(collectedResponses[4]);
                        decimal fats = Convert.ToDecimal(collectedResponses[5]);
                        decimal servingSize = Convert.ToDecimal(collectedResponses[6]);

                        Foods food = new Foods(0, name, category, calories, carbs, proteins, fats, servingSize);
                        int foodID = storageManager.InsertFood(food);

                        ShowMessage($"New food item added with ID: {foodID}. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input. Nutrition values must be numbers. Press any key to fill again.", 9);
                    }
                }
            }

            FoodOptions();
        }
        //adds a new diet plan by collecting target values for calories, proteins, carbs, and fats. Validates inputs, creates a new DietPlans object, inserts it into the database, and shows feedback.
        public void InsertNewDietPlan()
        {
            while (true)
            {
                view.Clear("Add New Diet Plan");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Diet Plan Name",
            "Target Calories",
            "Target Proteins (g)",
            "Target Carbohydrates (g)",
            "Target Fats (g)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "" ||
                    collectedResponses[2] == "" || collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 9);
                }
                else
                {
                    try
                    {
                        string planName = collectedResponses[0];
                        int calories = Convert.ToInt32(collectedResponses[1]);
                        int proteins = Convert.ToInt32(collectedResponses[2]);
                        int carbs = Convert.ToInt32(collectedResponses[3]);
                        int fats = Convert.ToInt32(collectedResponses[4]);

                        DietPlans plan = new DietPlans(0, planName, calories, proteins, carbs, fats);
                        int planID = storageManager.InsertDietPlan(plan);

                        ShowMessage($"New diet plan added with ID: {planID}. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input. Targets must be whole numbers. Press any key to fill again.", 9);
                    }
                }
            }

            DietPlansOptions();
        }
        //collects a new goal from the user, links it to a diet plan and current user, and saves it in the database.
        public void InsertNewGoal()
        {
            while (true)
            {
                view.Clear("Add New Goal");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Diet Plan ID",
            "Goal"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 2);
                }
                else
                {
                    try
                    {
                        int dietPlanID = Convert.ToInt32(collectedResponses[0]);
                        string goalText = collectedResponses[1];
                        string dateStarted = DateTime.Now.ToString("yyyy-MM-dd");
                        string dateEnded = null;

                        Goals goal = new Goals(0, TheID, dietPlanID, goalText, dateStarted, dateEnded);
                        int goalID = storageManager.InsertGoal(goal);

                        ShowMessage($"New goal added with ID: {goalID}. Press any key to continue.", 2);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input. Diet Plan ID must be a number. Press any key to fill again.", 2);
                    }
                }
            }

            GoalsOptions();
        }
        //adds a new daily log entry for the current user by collecting food ID and meal time, and saving it with the current date.
        public void InsertNewDailyLog()
        {
            while (true)
            {
                view.Clear("New Daily Log");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Log ID",
            "Meal Time (e.g. Breakfast, Lunch)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 2);
                }
                else
                {
                    try
                    {
                        int foodID = Convert.ToInt32(collectedResponses[0]);
                        string mealTime = collectedResponses[1];
                        string dateLogged = DateTime.Now.ToString("yyyy-MM-dd");

                        DailyLog log = new DailyLog(0, TheID, foodID, mealTime, dateLogged);
                        int logID = storageManager.InsertDailyLog(log);

                        ShowMessage($"New log added with ID: {logID}. Press any key to continue.", 2);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input. Log ID must be a number. Only breakfast, lunch , and dinner allowed as meal times. Food ID must also exist. Press any key to fill again.", 2);
                    }
                }
            }

            DailyLogOptions();
        }
        //handles login for existing users by validating user ID and password, then navigates to the user home page if successful
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
                }, this);

                try
                {
                    int userID = Convert.ToInt32(collectedResponses[0]);
                    string passwordkey = collectedResponses[1];
                    int result = storageManager.GetUserID(userID, passwordkey);
                    if (result == 0)
                    {
                        ShowMessage("Invalid ID and password combination, press any key to try again", 2);
                    }
                    else
                    {
                        userType = "user";
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
        //uuthenticates an admin by verifying the entered ID and password. If successful, sets admin session and goes to admin home page.
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
                }, this);

                try
                {
                    int adminID = Convert.ToInt32(collectedResponses[0]);
                    string passwordkey = collectedResponses[1];
                    int result = storageManager.GetAdminID(adminID, passwordkey);
                    if (result == 0)
                    {
                        ShowMessage("Invalid ID and password combination, press any key to try again", 2);
                    }
                    else
                    {
                        userType = "admin";
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
        //updates the details of an existing user based on form-style input. Validates inputs, handles errors, and saves changes to the database.
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
                }, this);

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
        //allows an existing admin to update their personal details (name, email, password). Validates inputs and updates the record in the database.
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
                }, this);

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
        //updates an existing food item in the database with new user-provided details (name, category, nutrition values). Validates all fields before saving.
        public void UpdateFood(int foodID)
        {
            ConsoleView view = new ConsoleView();
            Foods food = storageManager.GetFoodByID(foodID);

            while (true)
            {
                if (food == null)
                {
                    Console.WriteLine("Food item not found.");
                    return;
                }

                view.Clear("Update Food");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Food Name",
            "Category",
            "Calories (g)",
            "Carbohydrates (g)",
            "Proteins (g)",
            "Fats (g)",
            "Serving Size (g)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "" || collectedResponses[2] == "" ||
                    collectedResponses[3] == "" || collectedResponses[4] == "" || collectedResponses[5] == "" ||
                    collectedResponses[6] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 9);
                }
                else
                {
                    try
                    {
                        food.FoodName = collectedResponses[0];
                        food.Category = collectedResponses[1];
                        food.Calories = Convert.ToDecimal(collectedResponses[2]);
                        food.Carbohydrates = Convert.ToDecimal(collectedResponses[3]);
                        food.Proteins = Convert.ToDecimal(collectedResponses[4]);
                        food.Fats = Convert.ToDecimal(collectedResponses[5]);
                        food.ServingSize = Convert.ToDecimal(collectedResponses[6]);

                        storageManager.UpdateFood(food);
                        ShowMessage("Food item updated successfully. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Use numeric values where required. Press any key to fill again.", 9);
                    }
                }
            }

            FoodOptions();
        }
        //updates an existing diet plan in the database with new target values (name, calories, proteins, carbs, fats) provided by the user after input validation.
        public void UpdateDietPlan(int dietPlanID)
        {
            ConsoleView view = new ConsoleView();
            DietPlans plan = storageManager.GetDietPlanByID(dietPlanID);

            while (true)
            {
                if (plan == null)
                {
                    Console.WriteLine("Diet plan not found.");
                    return;
                }

                view.Clear("Update Diet Plan");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Diet Plan Name",
            "Calorie Target (g)",
            "Protein Target (g)",
            "Carbohydrate Target (g)",
            "Fat Target (g)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "" || collectedResponses[2] == "" ||
                    collectedResponses[3] == "" || collectedResponses[4] == "")
                {
                    ShowMessage("Required fields cannot be empty, press any key to fill again", 9);
                }
                else
                {
                    try
                    {
                        plan.DietPlan = collectedResponses[0];
                        plan.CaloriesTarget = Convert.ToInt32(collectedResponses[1]);
                        plan.ProteinsTarget = Convert.ToInt32(collectedResponses[2]);
                        plan.CarbohydratesTarget = Convert.ToInt32(collectedResponses[3]);
                        plan.FatsTarget = Convert.ToInt32(collectedResponses[4]);

                        storageManager.UpdateDietPlan(plan);
                        ShowMessage("Diet plan updated successfully. Press any key to continue.", 9);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Please enter valid numbers. Press any key to fill again.", 9);
                    }
                }
            }

            DietPlansOptions();
        }
        //udates an existing daily food log entry for the logged-in user by modifying the associated food ID, meal time, and date logged.
        public void UpdateDailyLog(int logID)
        {
            ConsoleView view = new ConsoleView();
            DailyLog log = storageManager.GetDailyLogByID(logID, TheID);

            while (true)
            {
                if (log == null)
                {
                    ShowMessage("Log not found or you are not authorized.", 2);
                    return;
                }

                view.Clear("Update Daily Log");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Food ID",
            "Meal Time (e.g. breakfast, lunch, dinner)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "")
                {
                    ShowMessage("All fields are required. Press any key to fill again.", 2);
                }
                else
                {
                    try
                    {
                        int foodID = Convert.ToInt32(collectedResponses[0]);
                        string mealTime = collectedResponses[1];
                        string dateLogged = DateTime.Now.ToString("yyyy-MM-dd");

                        log.FoodID = foodID;
                        log.MealTime = mealTime;
                        log.DateLogged = dateLogged;

                        storageManager.UpdateDailyLog(log);
                        ShowMessage("Daily log updated successfully. Press any key to continue.", 2);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Please try again.", 2);
                    }
                }
            }

            DailyLogOptions();
        }
        //updates an existing goal for the logged-in user, allowing changes to diet plan ID, goal description, and optional end date.
        public void UpdateGoal(int TheID)
        {
            ConsoleView view = new ConsoleView();
            Goals goal = storageManager.GetGoalByID(TheID, this.TheID);

            while (true)
            {
                if (goal == null)
                {
                    ShowMessage("Goal not found or access denied. Press any key to try again", 3);
                    Console.ReadKey(true);
                    GoalsOptions();
                    return;
                }

                view.Clear("Update Goal");
                Console.WriteLine();
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(press ctrl + enter to submit form)\n");
                Console.ResetColor();

                List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Diet Plan ID",
            "Goal",
            "Date Ended (yyyy-MM-dd or leave blank)"
                }, this);

                if (collectedResponses[0] == "" || collectedResponses[1] == "")
                {
                    ShowMessage("Required fields cannot be empty. Press any key to fill again.", 3);
                }
                else
                {
                    try
                    {
                        goal.DietPlanID = Convert.ToInt32(collectedResponses[0]);
                        goal.Goal = collectedResponses[1];
                        goal.DietPlanID = Convert.ToInt32(collectedResponses[0]);
                        goal.Goal = collectedResponses[1];

                        // Only set DateEnded if the user provided input
                        if (!string.IsNullOrWhiteSpace(collectedResponses[2]))
                        {
                            goal.DateEnded = DateTime.Parse(collectedResponses[2]).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            goal.DateEnded = "";
                        }

                        storageManager.UpdateGoal(goal);

                        ShowMessage("Goal updated successfully. Press any key to continue.", 3);
                        break;
                    }
                    catch
                    {
                        ShowMessage("Invalid input format. Diet Plan ID must be a number.", 3);
                    }
                }
            }

            GoalsOptions();
        }
        //prompts the user (or admin) to confirm deletion of a user account by ID. If confirmed and deletion is successful, the application exits (for users) or returns to the menu (for admins). If deletion fails, displays an error message and returns to the UserOptions menu.
        public void DeleteUser(int userID)
        {
            Menu confirmDelete;

            view.Clear("Delete User");
            if (userType == "admin")
            {
                confirmDelete = new Menu("Are you sure you want to delete this account?", new string[] { "Yes", "No" });
            }
            else
            {
                confirmDelete = new Menu("Are you sure you want to delete your account?", new string[] { "Yes", "No" });
            }
            
            int selectedIndex = confirmDelete.Run("Delete Account", userType, this);

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
        //deletes an admin account after confirmation. If the current logged-in admin deletes their own account, the application exits; otherwise, it returns to the admin options menu.
        public void DeleteAdmin(int adminID)
        {
            view.Clear("Delete Admin");
            Menu confirmDelete = new Menu("Are you sure you want to delete this admin account?", new string[] { "Yes", "No" });

            int selectedIndex = confirmDelete.Run("Delete Admin", userType, this);

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
        //confirms and deletes a specific allergy for the logged-in user, showing a success or failure message, then returns to the allergy options menu.
        public void DeleteAllergy(int allergyID)
        {
            Menu confirmDelete = new Menu("Are you sure you want to delete this allergy?", new string[] { "Yes", "No" });

            view.Clear("Delete Allergy");
            int selectedIndex = confirmDelete.Run("Delete Allergy", userType, this);

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteAllergyByID(allergyID, TheID);
                    view.Clear("Delete Allergy");

                    if (success)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The allergy has been successfully deleted.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to delete the allergy. It may not exist or you are not authorized.");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to go back...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    AllergiesOptions();
                    break;

                case 1:
                    AllergiesOptions();
                    break;
            }
        }
        //prompts for confirmation and deletes a food item from the database, displaying success or error messages, then returns to the food options menu.
        public void DeleteFood(int foodID)
        {
            view.Clear("Delete Food");

            Menu confirmDelete = new Menu("Are you sure you want to delete this food item?", new string[] { "Yes", "No" });
            int selectedIndex = confirmDelete.Run("Delete Food", userType, this);

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteFoodByID(foodID);
                    view.Clear("Delete Food");

                    if (success)
                    {
                        Console.WriteLine("This food item has been successfully deleted.");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred. The food item could not be deleted. It may not exist.");
                    }

                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey(true);
                    FoodOptions();
                    break;

                case 1:
                    FoodOptions();
                    break;
            }
        }
        //prompts the user for confirmation, attempts to delete the selected diet plan, and displays the result before returning to the diet plans menu.
        public void DeleteDietPlan(int dietPlanID)
        {
            view.Clear("Delete Diet Plan");

            Menu confirmDelete = new Menu("Are you sure you want to delete this diet plan?", new string[] { "Yes", "No" });
            int selectedIndex = confirmDelete.Run("Delete Diet Plan", userType, this);

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteDietPlanByID(dietPlanID);
                    view.Clear("Delete Diet Plan");

                    if (success)
                    {
                        Console.WriteLine("The diet plan has been successfully deleted.");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred. The diet plan could not be deleted. It may not exist.");
                    }

                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey(true);
                    DietPlansOptions();
                    break;

                case 1:
                    DietPlansOptions();
                    break;
            }
        }
        //prompts the user to confirm deletion, attempts to delete the specified goal if authorized, then displays the result and goes back to the goals menu.
        public void DeleteGoal(int goalID)
        {
            Menu confirmDelete = new Menu("Are you sure you want to delete this goal?", new string[] { "Yes", "No" });

            view.Clear("Delete Goal");
            int selectedIndex = confirmDelete.Run("Delete Goal", userType, this);

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteGoalByID(goalID, TheID);
                    view.Clear("Delete Goal");

                    if (success)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The goal has been successfully deleted.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to delete the goal. It may not exist or you are not authorized.");
                    }

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to go back...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    GoalsOptions(); 
                    break;

                case 1:
                    GoalsOptions();
                    break;
            }
        }
        //confirms deletion of the selected daily log, deletes it if the user is authorized, and then displays the result before returning to the daily log menu.
        public void DeleteDailyLog(int logID)
        {
            Menu confirmDelete = new Menu("Are you sure you want to delete this food log?", new string[] { "Yes", "No" });

            view.Clear("Delete Daily Log");
            int selectedIndex = confirmDelete.Run("Delete Daily Log", userType, this);

            switch (selectedIndex)
            {
                case 0:
                    bool success = storageManager.DeleteDailyLogByID(logID, TheID);
                    view.Clear("Delete Daily Log");

                    if (success)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The daily log has been successfully deleted.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to delete the daily log. It may not exist or you are not authorized.");
                    }

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to go back...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    DailyLogOptions();
                    break;

                case 1:
                    DailyLogOptions();
                    break;
            }
        }
        //displays user or admin options related to user accounts, such as viewing, updating, inserting, or deleting user information, based on the current user type.
        public void UserOptions()
        {
            if (userType == "user")
            {
                string prompt = "";
                string[] options = { "View your details", "Update your details", "Delete account" };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("Settings", userType, this);
                view.Clear("Settings");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.ViewAllUserDetails(userType, TheID);
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
                int SelectedIndex = mainMenu.Run("Manage Users", userType, this);
                view.Clear("Manage Users");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.ViewAllUserDetails(userType, TheID);
                        UserOptions();
                        break;
                    case 1:
                        InsertNewUser();
                        break;
                    case 2:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("\n(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "User ID to update: "
                    }, this);
                            try
                            {
                                int usersID = Convert.ToInt32(collectedResponses[0]);
                                UpdateUser(usersID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Update User");
                            }
                        }
                    case 3:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine();
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "User ID to delete: "
                    }, this);
                            try
                            {
                                int usersID = Convert.ToInt32(collectedResponses[0]);
                                DeleteUser(usersID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Delete User");
                            }
                        }
                }
            }
        }
        //presents admin-specific options to view, add, update, or delete admin accounts. Based on the user's menu selection, the appropriate action is triggered. Includes input validation and form-style input for update and delete operations. Loops allow retry on invalid inputs, and each action goes back to AdminOptions.
        public void AdminOptions()
        {
            string prompt = "";
            string[] options = { "View all admins", "Insert new admin", "Update existing admin", "Delete an admin"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("Manage Admins", userType, this);
            view.Clear("Manage Admins");

            switch (SelectedIndex)
            {
                case 0:
                    storageManager.ViewAllAdmins();
                    AdminOptions();
                    break;
                case 1:
                    InsertNewAdmin();
                    break;
                case 2:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Admin ID to update: "
                }, this);
                        try
                        {
                            int adminsID = Convert.ToInt32(collectedResponses[0]);
                            UpdateAdmin(adminsID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Update Admin");
                        }
                    }
                case 3:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Admin ID to delete: "
                }, this);
                        try
                        {
                            int adminsID = Convert.ToInt32(collectedResponses[0]);
                            DeleteAdmin(adminsID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Delete Admin");
                        }
                    }
            }
        }
        //displays the allergy management menu for the current user, allowing them to view, add, or delete allergies. Based on the selected menu option, it routes to the appropriate handler. For deletion, it prompts for Allergy ID with input validation, and repeats on invalid input.
        public void AllergiesOptions()
        {
            string prompt = "";
            string[] options = { "View all allergies", "Insert new allergy",  "Delete an allergy"};
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("Manage Allergies ", userType, this);
            view.Clear("Manage Allergies");

            switch (SelectedIndex)
            {
                case 0:
                    storageManager.ViewAllAllergies(TheID);
                    AllergiesOptions();
                    break;
                case 1:
                    InsertNewAllergy();
                    break;
                case 2:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Allergy ID to delete: "
                }, this);
                        try
                        {
                            int allergyID = Convert.ToInt32(collectedResponses[0]);
                            DeleteAllergy(allergyID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Delete Allergy");
                        }
                    }                    
            }
        }
        //displays the food management interface based on user role. Regular users can only view all foods. Admins can view, insert, update, or delete food items. For update and delete actions, it prompts the admin to enter a valid Food ID and handles incorrect input by showing error messages and retrying the input process in a loop.
        public void FoodOptions()
        {
            if (userType == "user")
            {               
                storageManager.ViewAllFoods();
                UserHomePage();
            }
            else
            {
                string prompt = "";
                string[] options = { "View all foods", "Insert new food", "Update existing food", "Delete a food"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("", userType, this);
                view.Clear("");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.ViewAllFoods();
                        FoodOptions();
                        break;
                    case 1:
                        InsertNewFood();
                        break;
                    case 2:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine();
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "Food ID to update"
                    }, this);
                            try
                            {
                                int foodID = Convert.ToInt32(collectedResponses[0]);
                                UpdateFood(foodID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Update Food");
                            }
                        }
                    case 3:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine();
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "Food ID to delete"
                    }, this);
                            try
                            {
                                int foodID = Convert.ToInt32(collectedResponses[0]);
                                DeleteFood(foodID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Delete Food");
                            }
                        }
                }
            }
        }
        //presents diet plan management options based on user role. Regular users can only view all diet plans and are redirected to the homepage afterward. Admins can view, insert, update, or delete diet plans. For update and delete actions, the method collects a Diet Plan ID from the admin, validates it, and proceeds accordingly. Invalid inputs are caught and handled with appropriate error messages in a retry loop.
        public void DietPlansOptions()
        {
            if (userType == "user")
            {
                storageManager.ViewAllDietPlans();
                UserHomePage();
            }
            else
            {
                string prompt = "";
                string[] options = { "View all diet plans", "Insert new diet plan", "Update existing diet plan", "Delete a diet plan"};
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("", userType, this);
                view.Clear("");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.ViewAllDietPlans();
                        DietPlansOptions();
                        break;
                    case 1:
                        InsertNewDietPlan();
                        break;
                    case 2:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine();
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "Diet Plan ID to update"
                    }, this);
                            try
                            {
                                int dietPlanID = Convert.ToInt32(collectedResponses[0]);
                                UpdateDietPlan(dietPlanID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Update Diet Plan");
                            }
                        }
                    case 3:
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine();
                            Console.SetCursorPosition(50, Console.CursorTop);
                            Console.WriteLine("(press ctrl + enter to submit form)\n");
                            Console.ResetColor();
                            List<string> collectedResponses = InputManager.GetInput(new string[]
                    {
            "Diet Plan ID to delete"
                    }, this);
                            try
                            {
                                int dietPlanID = Convert.ToInt32(collectedResponses[0]);
                                DeleteDietPlan(dietPlanID);
                            }
                            catch (Exception ex)
                            {
                                ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                                Console.ReadKey(true);
                                view.Clear("Delete Diet Plan");
                            }
                        }
                }
            }
        }
        //displays the goal management menu for users, allowing them to view, add, update, or delete goals with input validation and navigation support.
        public void GoalsOptions()
        {
            string prompt = "";
            string[] options = { "View all goals", "Insert new goal", "Update existing goal", "Delete a goal" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("", userType, this);
            view.Clear("");

            switch (SelectedIndex)
            {
                case 0:
                    storageManager.ViewAllGoals(TheID);
                    GoalsOptions();
                    break;
                case 1:
                    InsertNewGoal();
                    break;
                case 2:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Gaol ID to update"
                }, this);
                        try
                        {
                            int goalID = Convert.ToInt32(collectedResponses[0]);
                            UpdateGoal(goalID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Update Goal");
                        }
                    }
                case 3:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Goal ID to delete"
                }, this);
                        try
                        {
                            int goalID = Convert.ToInt32(collectedResponses[0]);
                            DeleteGoal(goalID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Delete Goal");
                        }
                    }
            }
        }
        //displays the daily log management menu for users, allowing them to view, insert, update, or delete their food intake logs with proper input validation and control navigation.
        public void DailyLogOptions()
        {
            string prompt = "";
            string[] options = { "View all daily logs", "Insert new daily log", "Update existing daily log", "Delete a daily log" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run("", userType, this);
            view.Clear("");

            switch (SelectedIndex)
            {
                case 0:
                    storageManager.ViewAllDailyLogs(TheID);
                    DailyLogOptions();
                    break;
                case 1:
                    InsertNewDailyLog();
                    break;
                case 2:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Daily Log ID to update"
                }, this);
                        try
                        {
                            int logID = Convert.ToInt32(collectedResponses[0]);
                            UpdateDailyLog(logID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Update Daily Log");
                        }
                    }
                case 3:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.SetCursorPosition(50, Console.CursorTop);
                        Console.WriteLine("(press ctrl + enter to submit form)\n");
                        Console.ResetColor();
                        List<string> collectedResponses = InputManager.GetInput(new string[]
                {
            "Daily Log ID to delete"
                }, this);
                        try
                        {
                            int logID = Convert.ToInt32(collectedResponses[0]);
                            DeleteDailyLog(logID);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage("ID must be a number and cannot be null, press any key to go back", 2);
                            Console.ReadKey(true);
                            view.Clear("Delete Daily Log");
                        }
                    }
            }
        }
        //displays user-specific or admin-specific analytical report options and goes back to the data queries in StorageManager, allowing for users to see some insights :).
        public void QueryOptions()
        {
            if (userType == "user")
            {
                string prompt = "";
                string[] options = { "Food by category", "View goals", "Detailed user info", "Food by calories", "Top 10 foods", "Diet plan targets", "Food related to allergies", "Popular diet goal combos" };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("View Reports", userType, this);
                view.Clear("View Reports");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.ViewFoodsByCategory();
                        QueryOptions();
                        break;
                    case 1:
                        storageManager.ViewUserGoals(TheID);
                        QueryOptions();
                        break;
                    case 2:
                        storageManager.DetailedUserInfo(TheID, userType);
                        QueryOptions();
                        break;
                    case 3:
                        storageManager.FoodByCalories();
                        QueryOptions();
                        break;
                    case 4:
                        storageManager.Top10Foods();
                        QueryOptions();
                        break;
                    case 5:
                        storageManager.DietPlanTargets();
                        QueryOptions();
                        break;
                    case 6:
                        storageManager.FoodsRelatedtoAllergies(TheID);
                        QueryOptions();
                        break;
                    case 7:
                        storageManager.DietGoalCombos();
                        QueryOptions();
                        break;
                }
            }
            else
            {
                string prompt = "";
                string[] options = { "Calculate user BMI", "Top 25 users", "User demographic", "Detailed user info", "Average goal durations", "Average calories per meal time", "Meal times logged", "Users' average calories" };
                Menu mainMenu = new Menu(prompt, options);
                int SelectedIndex = mainMenu.Run("View Reports", userType, this);
                view.Clear("View Reports");

                switch (SelectedIndex)
                {
                    case 0:
                        storageManager.CalculateBMI();
                        QueryOptions();
                        break;
                    case 1:
                        storageManager.Top25Users();
                        QueryOptions();
                        break;
                    case 2:
                        storageManager.UserDemographic();
                        QueryOptions();
                        break;
                    case 3:
                        storageManager.DetailedUserInfo(TheID, userType);
                        QueryOptions();
                        break;
                    case 4:
                        storageManager.AverageGoalDurations();
                        QueryOptions();
                        break;
                    case 5:
                        storageManager.AverageCaloriesPerMealTime();
                        QueryOptions();
                        break;
                    case 6:
                        storageManager.MealTimesLogged();
                        QueryOptions();
                        break;
                    case 7:
                        storageManager.UserAverageCalories();
                        QueryOptions();
                        break;
                }
            }
        }
        //shows messages in forms with special formatting relative to the number of fields in that form.
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
        //called to close the connecttion to the database once the user decides to exit. It is called once they use the exit method.
        public void CloseConnection()
        {
            if (storageManager.conn != null && storageManager.conn.State == ConnectionState.Open)
            {
                storageManager.conn.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
