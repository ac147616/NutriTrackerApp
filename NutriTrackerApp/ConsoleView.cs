using NutriTrackerApp;
using System;
//Change all hello's to welcome maybe
//Edit the password so you cannot see it
//use set cursor position to let them choose what they want to enter
//use up or down to navigate

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
