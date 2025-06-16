using NutriTrackerApp;
using System;
//Change all hello's to welcome maybe
//Edit the password so you cannot see it
//use set cursor position to let them choose what they want to enter
//use up or down to navigate

public class ConsoleView
{

	public void DisplayFoods(List<Food> foods)
	{
		foreach (Food food in foods)
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

	public static void Help()
	{
		Console.WriteLine("blah blah");
	}

	public void Clear()
	{
		Console.Clear();
		string title = @"
███╗   ██╗██╗   ██╗████████╗██████╗ ██╗████████╗██╗ ██████╗ ███╗   ██╗    ████████╗██████╗  █████╗  ██████╗██╗  ██╗███████╗██████╗ 
████╗  ██║██║   ██║╚══██╔══╝██╔══██╗██║╚══██╔══╝██║██╔═══██╗████╗  ██║    ╚══██╔══╝██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗
██╔██╗ ██║██║   ██║   ██║   ██████╔╝██║   ██║   ██║██║   ██║██╔██╗ ██║       ██║   ██████╔╝███████║██║     █████╔╝ █████╗  ██████╔╝
██║╚██╗██║██║   ██║   ██║   ██╔══██╗██║   ██║   ██║██║   ██║██║╚██╗██║       ██║   ██╔══██╗██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗
██║ ╚████║╚██████╔╝   ██║   ██║  ██║██║   ██║   ██║╚██████╔╝██║ ╚████║       ██║   ██║  ██║██║  ██║╚██████╗██║  ██╗███████╗██║  ██║
╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝       ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═";

        Console.WriteLine(title);
    }


}

