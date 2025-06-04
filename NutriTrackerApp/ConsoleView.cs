using NutriTrackerApp;
using System;

public class ConsoleView
{
	public string LogIn()
	{
		Console.WriteLine("Hey! How would you like to use this interface?");
		Console.WriteLine("\n[1] I am a user");
        Console.WriteLine("[2] I am an admin");
		Console.Write("\nEnter your option: ");

        return Console.ReadLine();
	}

	public string DisplayMenu()
	{
		Console.WriteLine("Welcome to my Health & Nutrion Tracker");
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
