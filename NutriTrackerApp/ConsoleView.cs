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
		Clear("Help");
		Console.WriteLine("\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\r\n");
	}

	public void Clear(string nameOfPage)
	{
		Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string title = @"
███╗  ██╗██╗   ██╗████████╗██████╗ ██╗  ████████╗██████╗  █████╗  █████╗ ██╗  ██╗███████╗██████╗ 
████╗ ██║██║   ██║╚══██╔══╝██╔══██╗██║  ╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝██╔════╝██╔══██╗
██╔██╗██║██║   ██║   ██║   ██████╔╝██║     ██║   ██████╔╝███████║██║  ╚═╝█████═╝ █████╗  ██████╔╝
██║╚████║██║   ██║   ██║   ██╔══██╗██║     ██║   ██╔══██╗██╔══██║██║  ██╗██╔═██╗ ██╔══╝  ██╔══██╗
██║ ╚███║╚██████╔╝   ██║   ██║  ██║██║     ██║   ██║  ██║██║  ██║╚█████╔╝██║ ╚██╗███████╗██║  ██║
╚═╝  ╚══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝     ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝";

        int consoleWidth = Console.WindowWidth;
        string[] eachLine = title.Split('\n');
        foreach (string aLine in eachLine)
        {
            string trimmed = aLine.TrimEnd('\r');
            int padding = (consoleWidth - trimmed.Length) / 2;
            Console.SetCursorPosition(Math.Max(0, padding), Console.CursorTop);
            Console.WriteLine(trimmed);
        }

		Console.WriteLine();
        Console.WriteLine("+" + new string('-', consoleWidth - 2) + "+");

 
        string left = "[Back: Ctrl + B]";
        string right = "[Help: Ctrl + H] [Exit: Ctrl + E]";

        int spaceForCenter = consoleWidth - left.Length - right.Length - 4;
        string center = "";

        if (nameOfPage.Length <= spaceForCenter)
        {
            int paddingLeft = (spaceForCenter - nameOfPage.Length) / 2;
            center = new string(' ', paddingLeft) + nameOfPage + new string(' ', spaceForCenter - paddingLeft - nameOfPage.Length);
        }
        else
        {
            center = new string(' ', spaceForCenter);
        }

        string headerLine = "| " + left + center + right + " |";
        Console.WriteLine(headerLine);
        Console.WriteLine("+" + new string('-', consoleWidth - 2) + "+");
    }
}