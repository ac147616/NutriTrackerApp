using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NutriTrackerApp;

namespace NutriTrackerApp
{
    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 1;
        }
        private void DisplayOptions()
        {
            if (Prompt != "")
            {
                Console.WriteLine();
                Console.WriteLine(new string(' ', Math.Max(0, (Console.WindowWidth - Prompt.Length) / 2)) + Prompt);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
            }

                for (int i = 0; i < Options.Length; i++)
                {
                    string currentOption = Options[i];

                    if (i == SelectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    string menuItem = $"<< {currentOption} >>";
                    int windowWidth = Console.WindowWidth;
                    int x = Math.Max(0, (windowWidth - menuItem.Length) / 2);
                    int y = Console.CursorTop;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(menuItem);
                }

            Console.ResetColor();
        }
        public int Run(string pageName, string actualUserType, Program myProgram)
        {
            //We need a loop structure so that everytime the user clicks a key it re-renders the console view
            ConsoleKey keyPressed;
            do
            {
                ConsoleView view = new ConsoleView();
                view.Clear(pageName);
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (keyPressed == ConsoleKey.B)
                    {
                        Menu.Back(myProgram);
                        return -1;
                    }
                    else if (keyPressed == ConsoleKey.H)
                    {
                        Menu.GetHelp(myProgram);
                        return -1;
                    }
                    else if (keyPressed == ConsoleKey.E)
                    {
                        Menu.Exit(myProgram);
                        return -1;
                    }
                }

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
        public static void Back(Program myProgram)
        {
            if (myProgram.userType == "admin")
            {
                myProgram.AdminHomePage();
            }
            else if (myProgram.userType == "user")
            {
                myProgram.UserHomePage();
            }
            else
            {
                myProgram.RunUserTypeMenu2();
            }
        }
        public static void GetHelp(Program myProgram)
        {
            ConsoleView view = new ConsoleView();
            view.Help();
            Console.ReadKey(true);
            Back(myProgram);
        }
        public static void Exit(Program myProgram)
        {
            ConsoleView view = new ConsoleView();
            view.Clear("Exit");
            Menu mainMenu = new Menu("\nAre you sure you want to exit the application?\n", ["yes", "no"]);
            int SelectedIndex = mainMenu.Run(" ", myProgram.userType, myProgram);

            switch (SelectedIndex)
            {
                case 0:
                    view.Clear("Exit");
                    Console.WriteLine("\n Thank you for using the NutriTracker, adios!\n");
                    System.Environment.Exit(0);
                    break;
                case 1:
                    Back(myProgram);
                    break;
            }
        }
    }
}
