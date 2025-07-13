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

            // this stores the y positon of each label of a field
            int[] yRowPosition = new int[labels.Length];
            int spaceFromLeft = 40; //this is the distance from the left that i want for all the labels
            int spaceFromTop = 14; //this is where i want the first field to start from.

            //looping through each label and printing each one at its own position
            for (int i = 0; i < labels.Length; i++)
            {
                int top = spaceFromTop + (i * 2);  // space out fields vertically
                yRowPosition[i] = top; //storing last y position

                Console.SetCursorPosition(spaceFromLeft, top);
                Console.Write($"{labels[i]}: ");
            }

            int currentField = 0; //this is the field the user is currently editing

            //keeping all input until the form is submitted
            while (true)
            {
                int inputLeftPadding = spaceFromLeft + labels[currentField].Length + 2;
                int inputTopPadding = yRowPosition[currentField];

                //clear any previous charcaters on this field
                Console.SetCursorPosition(inputLeftPadding, inputTopPadding);
                Console.Write(new string(' ', Console.WindowWidth - inputLeftPadding - 1));

                //write the value again after clearing
                Console.SetCursorPosition(inputLeftPadding, inputTopPadding);

                //just hashing the field input if its a password
                if (labels[currentField].ToLower().Contains("password") || (labels[currentField].ToLower().Contains("confirm password")))
                {
                    Console.Write(new string('*', collectedResponses[currentField].Length));
                }
                else
                {
                    Console.Write(collectedResponses[currentField]);
                }

                Console.SetCursorPosition(inputLeftPadding + collectedResponses[currentField].Length, inputTopPadding); //move cursor to end of the line

                ConsoleKeyInfo key = Console.ReadKey(true);  //get what kind of key they pressed

                //check if its any of the shortcuts that I have made available
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

                //navigate down or move to next field
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField + 1) % labels.Length;
                }
                //navigate up
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField - 1 + labels.Length) % labels.Length;
                }
                //delete last character
                else if (key.Key == ConsoleKey.Backspace && collectedResponses[currentField].Length > 0)
                {
                    collectedResponses[currentField] = collectedResponses[currentField][..^1]; // slice to remove last char
                }
                //accepting regular typing characters
                else if (!char.IsControl(key.KeyChar))
                {
                    collectedResponses[currentField] += key.KeyChar;
                }

                //finally, submit the form if ctrl enter is pressed
                if (key.Key == ConsoleKey.Enter && (key.Modifiers & ConsoleModifiers.Control) != 0)
                    break;
            }

            return collectedResponses; //return the input collected from user to program.cs
        }

        //method called when ctrl b is pressed
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

        //method called if ctrl h is pressed
        public static void GetHelp(Program myProgram)
        {
            ConsoleView view = new ConsoleView();
            view.Help();
            Console.ReadKey(true);
            Back(myProgram);
        }

        //method called if ctrl e is pressed
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
                    myProgram.CloseConnection();
                    System.Environment.Exit(0);
                    break;
                case 1:
                    Back(myProgram);
                    break;
            }
        }

    }
}
