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
            //string usertype = view.LogIn();

            switch (usertype)
            {
                case "1":
                    {
                        //NewUser();
                        break;
                    }
                case "2":
                    {
                        //ExistingUser();
                        break;
                    }
                case "3":
                    {
                        //AdminLogIn();
                        break;
                    }
            }

            //string choice = view.DisplayMenu();

            //switch (choice)
            //{
            //    case "1":
            //        {
            //            List<Food> foods = storageManager.GetAllFoods();
            //            view.DisplayFoods(foods);
            //        }
            //        break;
            //    case "2":
            //        {
            //            UpdateFoodName();
            //            break;
            //        }
            //    case "3":
            //        {
            //            InsertNewFood();
            //            break;
            //        }
            //    case "4":
            //        {
            //            DeleteFoodByName();
            //            break;
            //        }
            //    case "5":
            //        {
            //            //End Loop
            //            break;
            //        }
            //    default:
            //        Console.WriteLine("Invalid option. Please try again.");
            //        break;
            //}

            storageManager.CloseConnection();
        }

        public static void NewUser()
        { 
            view.DisplayMessage("Hello new user!\n\nEnter first name: ");
            string firstName = view.GetInput();
            view.DisplayMessage("Enter last name: ");
            string lastName = view.GetInput();
            view.DisplayMessage("Enter email address: ");
            string emailID = view.GetInput();
            view.DisplayMessage("Enter password: ");
            string passwordkey = view.GetInput();
            view.DisplayMessage("Enter age (or skip by pressing enter): ");
            int age = view.GetIntInput();
            view.DisplayMessage("Enter gender (or skip by pressing enter): ");
            string gender = view.GetInput();
            view.DisplayMessage("Enter your weight (or skip by pressing enter): ");
            double userWeight = Convert.ToDouble(view.GetInput());
            view.DisplayMessage("Enter your height (or skip by pressing enter): ");
            double userHeight = Convert.ToDouble(view.GetInput());
            view.DisplayMessage("Enter today's date (YYYY/MM/DD): ");
            string[] template = view.GetInput().Split('/');
            DateOnly signUpDate = new DateOnly(Convert.ToInt32(template[0]), Convert.ToInt32(template[1]), Convert.ToInt32(template[2]));
            int userID = 0;
            UserDetails user1 = new UserDetails(userID, firstName, lastName, emailID, passwordkey, age, gender, userWeight, userHeight, signUpDate);
            int generatedId = storageManager.InsertUserDetails(user1);
            view.DisplayMessage($"New user created with ID: {generatedId}");

        }
        public (int, string) ExistingUserLogIn()
        {
            Console.WriteLine("Hello user!\n\nEnter ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string passwordkey = Console.ReadLine();

            return (userID, passwordkey);
        }

        public (int, string) AdminLogIn()
        {
            Console.WriteLine("Hello admin!\n\nEnter ID: ");
            int adminID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string passwordkey = Console.ReadLine();

            return (adminID, passwordkey);
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
