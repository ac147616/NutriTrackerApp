using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NutriTrackerApp; //importing this because its needed to use the methods and variables from this namespace

namespace NutriTrackerApp
{
    // This creates interactive console menus for users to use up and down arrow keys to navigate options with and select them by pressing enter. As it is a reusable class it takes in two things: prompt and options. 
    class Menu
    {
        private int SelectedIndex; //to track the currently highlighted option
        private string[] Options; //to store the list of options given by program.cs
        private string Prompt; //to store the optional prompt that could display at the top of each menu

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0; //setting it to highlight the first option as the menu first displays
        }
        private void DisplayOptions()
        {
            //printing the prompt only if there is one
            if (Prompt != "")
            {
                Console.WriteLine();
                Console.WriteLine(new string(' ', Math.Max(0, (Console.WindowWidth - Prompt.Length) / 2)) + Prompt);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(); //to create padding betwen the header and menu
            }
                
                //this is a loop to display all options
                for (int i = 0; i < Options.Length; i++)
                {
                    string currentOption = Options[i];
                    
                    //changing the look of the selected option so its easy to tell and user doesnt have to remember
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

                    //i decided to add some details like <<option>> to give it more of a menu look
                    string menuItem = $"<< {currentOption} >>";
                    int windowWidth = Console.WindowWidth;
                    int x = Math.Max(0, (windowWidth - menuItem.Length) / 2); //centering it, x position
                    int y = Console.CursorTop; //y position is same
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(menuItem);
                }

            Console.ResetColor();
        }
        public int Run(string pageName, string actualUserType, Program myProgram)
        {
            ConsoleKey keyPressed; //to store the key pressed by the user

            do
            {
                ConsoleView view = new ConsoleView();
                view.Clear(pageName);
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key; //getting what key they pressed

                //handling the special shortcuts that I made available: back, help and exit
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (keyPressed == ConsoleKey.B)
                    {
                        Menu.Back(myProgram);
                        return -1; //-1 because its not used in switch cases
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
                    SelectedIndex--;  //moving up
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1; //creating a loop so if they click up on the top option it goes to last option
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++; //moving down
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0; //creating loop so if they click down on the last option it goes to top option
                    }
                }
            }
            while (keyPressed != ConsoleKey.Enter); //keep doing this until they actually select an option

            return SelectedIndex;//return this to the switch cases in program.cs can function.
        }

        //This is the method called when ctrl + B is pressed
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
                myProgram.RunUserTypeMenu2(); //duplicate of RunUserTypeMenu() but using a copy because cannot call a static method here (this is the non-static version of it)
            }
        }

        //This is the method called when Ctrl + H is pressed
        public static void GetHelp(Program myProgram)
        {
            ConsoleView view = new ConsoleView();
            view.Help(); //displaying user manual
            Console.ReadKey(true);
            Back(myProgram);
        }
        
        //This is the method called when Ctrl + E is pressed
        public static void Exit(Program myProgram)
        {
            ConsoleView view = new ConsoleView();
            view.Clear("Exit");

            //confirming
            Menu mainMenu = new Menu("Are you sure you want to exit the application?\n", ["yes", "no"]);
            int SelectedIndex = mainMenu.Run(" ", myProgram.userType, myProgram);

            switch (SelectedIndex)
            {
                case 0: //chose yes
                    view.Clear("Exit");
                    Console.WriteLine(new string(' ', Math.Max(0, (Console.WindowWidth - "Thank you for using the NutriTracker, adios!".Length) / 2)) + "Thank you for using the NutriTracker, adios!");
                    myProgram.CloseConnection();
                    System.Environment.Exit(0);
                    break;
                case 1: //chose no
                    Back(myProgram);
                    break;
            }
        }
    }
}
