using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp //This class manages how to make the input look like and work like a form
{
    class InputManager
    {
        public static List<string> GetInput(string[] labels)
        {
            //This is creates an empty list with the same amount of empty values as number of responses expected. It will store responses.
            Program myProgram = new Program();
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
                        myProgram.Back();  // or call a delegate like OnBackPressed()
                        return null;   // cancel form input if necessary
                    }
                    else if (key.Key == ConsoleKey.H)
                    {
                        myProgram.GetHelp();
                        // optionally: continue; if you want them to resume filling the form
                    }
                    else if (key.Key == ConsoleKey.E)
                    {
                        myProgram.Exit();
                        return null;   // stop the form and exit
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
    }
}
