using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp //This class manages how to make the input look like and work like a form
{
    class InputManager
    {
        public static List<string> GetInput(string[] labels, Program myProgram)
        {
            //This is creates an empty list with the same amount of empty values as number of responses expected. It will store responses.
            List<string> collectedResponses = new List<string>();
            for (int i = 0; i < labels.Length; i++)
                collectedResponses.Add("");

            // Stores the Y-position (row) of each field
            int[] yRowPosition = new int[labels.Length];

            int spaceFromLeft = 40;  // X-position of field labels
            int spaceFromTop = 14;    // Y-position of the first field

            for (int i = 0; i < labels.Length; i++)
            {
                int top = spaceFromTop + (i * 2);  // space out fields vertically
                yRowPosition[i] = top;

                Console.SetCursorPosition(spaceFromLeft, top);
                Console.Write($"{labels[i]}: ");
            }

            int currentField = 0; //This is the field the user will be editing

            while (true)
            {
                int inputLeftPadding = spaceFromLeft + labels[currentField].Length + 2;
                int inputTopPadding = yRowPosition[currentField];

                //Clear any previous charcaters on this field
                Console.SetCursorPosition(inputLeftPadding, inputTopPadding);
                Console.Write(new string(' ', Console.WindowWidth - inputLeftPadding - 1));

                // Write the current value again after clearing the field
                Console.SetCursorPosition(inputLeftPadding, inputTopPadding);

                if (labels[currentField].ToLower().Contains("password") || (labels[currentField].ToLower().Contains("confirm password")))
                {
                    Console.Write(new string('*', collectedResponses[currentField].Length));
                }
                else
                {
                    Console.Write(collectedResponses[currentField]);
                }

                Console.SetCursorPosition(inputLeftPadding + collectedResponses[currentField].Length, inputTopPadding); //Move cursor to end of the line

                ConsoleKeyInfo key = Console.ReadKey(true);  // Get what kind of key they pressed
                if ((key.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (key.Key == ConsoleKey.B)
                    {
                        Back(myProgram);
                        return null;
                    }
                    else if (key.Key == ConsoleKey.H)
                    {
                        GetHelp(myProgram);
                    }
                    else if (key.Key == ConsoleKey.E)
                    {
                        Exit(myProgram);
                        return null;
                    }
                }

                // Navigate down or move to next field
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField + 1) % labels.Length;
                }
                // Navigate up
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField - 1 + labels.Length) % labels.Length;
                }
                // Delete last character
                else if (key.Key == ConsoleKey.Backspace && collectedResponses[currentField].Length > 0)
                {
                    collectedResponses[currentField] = collectedResponses[currentField][..^1]; // slice to remove last char
                }
                // Accept regular typing characters
                else if (!char.IsControl(key.KeyChar))
                {
                    collectedResponses[currentField] += key.KeyChar;
                }

                // Submit form if Ctrl+Enter is pressed
                if (key.Key == ConsoleKey.Enter && (key.Modifiers & ConsoleModifiers.Control) != 0)
                    break;
            }

            return collectedResponses;  // Return the input collected from user
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
