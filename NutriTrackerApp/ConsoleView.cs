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

	public void Help()
	{
		Clear();
		Console.WriteLine("\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\r\n");
	}

	public void Clear()
	{
		Console.Clear();
		int consoleWidth = Console.WindowWidth;
		string title = @"
███╗  ██╗██╗   ██╗████████╗██████╗ ██╗  ████████╗██████╗  █████╗  █████╗ ██╗  ██╗███████╗██████╗ 
████╗ ██║██║   ██║╚══██╔══╝██╔══██╗██║  ╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝██╔════╝██╔══██╗
██╔██╗██║██║   ██║   ██║   ██████╔╝██║     ██║   ██████╔╝███████║██║  ╚═╝█████═╝ █████╗  ██████╔╝
██║╚████║██║   ██║   ██║   ██╔══██╗██║     ██║   ██╔══██╗██╔══██║██║  ██╗██╔═██╗ ██╔══╝  ██╔══██╗
██║ ╚███║╚██████╔╝   ██║   ██║  ██║██║     ██║   ██║  ██║██║  ██║╚█████╔╝██║ ╚██╗███████╗██║  ██║
╚═╝  ╚══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝     ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝";

		string[] lines = title.Split('\n');
		foreach (string line in lines)
		{
			string trimmed = line.Trim('\r');
            int padding = (consoleWidth - trimmed.Length) / 2;
            Console.SetCursorPosition(Math.Max(0, padding), Console.CursorTop);
            Console.WriteLine(trimmed);
        }

		Console.WriteLine();

        string back = "[Back: CTRL + B]";
        string help = "[Help: CTRL + H]";
        string exit = "[Exit: CTRL + E]";

        int spacing = consoleWidth - (back.Length + help.Length + exit.Length);
        if (spacing < 2) spacing = 2;
        string spacer = new string(' ', spacing - 2);

        Console.WriteLine($"{back}{spacer}{help} {exit}");
    }
}