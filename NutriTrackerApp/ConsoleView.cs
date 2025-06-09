using NutriTrackerApp;
using System;
//Change all hello's to welcome maybe
//Edit the password so you cannot see it
//use set cursor position to let them choose what they want to enter
//use up or down to navigate

public class ConsoleView
{
    private int SelectedIndex;
    private string[] Options;
    private string Prompt;
    public void Menu(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectedIndex = 1;
    }
    private void DisplayOptions()
    {
        Console.WriteLine(Prompt);
        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix;

            if (i == SelectedIndex)
            {
                prefix = "*";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = " ";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{prefix} << {currentOption} >>");
        }

        Console.ResetColor();
    }

    public int Run()
    {
        //We need a loop structure so that everytime the user clicks a key it re-renders the console view
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            DisplayOptions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            //Update SelectedIndex based on arrow keys.
            if (keyPressed == ConsoleKey.UpArrow)
            {
                SelectedIndex--;  //Selected index closer to the front of the array
                if (SelectedIndex == -1)
                {
                    SelectedIndex = Options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                SelectedIndex++;
                if (SelectedIndex == Options.Length)
                {
                    SelectedIndex = 0;
                }
            }
        }
        while (keyPressed != ConsoleKey.Enter);

        return SelectedIndex;
    }

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
