using NutriTrackerApp;
using System;

public class ConsoleView
{
	public string LogIn()
	{
		Console.WriteLine("Hey! How would you like to use this interface?");
		Console.WriteLine("\n[1] New user");
        Console.WriteLine("[2] Existing user");
        Console.WriteLine("[3] Admin");
		Console.Write("\nEnter your option: ");

        return Console.ReadLine();
	}

    public (string, string, string, string, int, string, double, double, DateOnly) NewUser()
    {
        Console.WriteLine("Hello user!\n\nEnter first name: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter last name: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter email address: ");
        string emailID = Console.ReadLine();
        Console.WriteLine("Enter password: ");
        string passwordkey = Console.ReadLine();
        Console.WriteLine("Enter age (or skip by pressing enter): ");
        int age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter gender (or skip by pressing enter): ");
        string gender = Console.ReadLine();
        Console.WriteLine("Enter your weight (or skip by pressing enter): ");
        double userWeight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Enter your height (or skip by pressing enter): ");
        double userHeight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Enter today's date (YYYY/MM/DD): ");
        string[] template = Console.ReadLine().Split('/');
        DateOnly signUpDate = new DateOnly(Convert.ToInt32(template[0]), Convert.ToInt32(template[1]), Convert.ToInt32(template[2]));


        return (firstName, lastName, emailID, passwordkey, age, gender, userWeight, userHeight, signUpDate);
    }
    
    public string, string, string, int? ExistingUserLogIn()
    {
        //Console.WriteLine("Hello user! Please choose one of the options:");
        //string firstName;
        //string lastName;
        //string passwordkey;


        ////additional enter ID if there are more than 1 maybe?
        
        //return  Console.ReadLine();
    }

    public string AdminLogIn()
    {
        Console.WriteLine("Hello user! Please choose one of the options:");

        return Console.ReadLine();
    }

    public string DisplayMenu()
	{
	    //some sort of heading like health and nutrition etc and options for pressing x and home at all times so will need to include in all functions
        Console.WriteLine("Welcome PUT USER's NAME HERE");
		Console.WriteLine("Menu: ");
		Console.WriteLine("1. View All records in foods");

		return Console.ReadLine();
	}

	public void DisplayFoods(List<Food> foods)
	{
		foreach(Food food in foods)
		{
			Console.WriteLine($"{food.FoodID}, {food.FoodName}, {food.Category}, {food.Calories}, {food.Carbohydrates}, {food.Proteins}, {food.Fats}, {food.ServingSize}");
		}
	}

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string GetInput()
    {
		return Console.ReadLine();
    }

    public int GetIntInput()
    {
		return Convert.ToInt32(Console.ReadLine());
    }
}
