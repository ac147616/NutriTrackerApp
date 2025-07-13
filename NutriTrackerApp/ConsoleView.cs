using NutriTrackerApp; //This imports the NutriTrackerApp namespae so that i can use methods and variables from it.
using System;


public class ConsoleView
{
    //This method is called to show instructions when the user chooses to view help.
	public void Help()
	{
        Clear("Help"); //Clear method to clear the console and reprint the header
        
        //The next set of lines just print a user manual which can be helpful for users if they get stuck. It simply uses Console.WriteLine()
        Console.WriteLine();
        Console.WriteLine("Welcome to NutriTrackerApp! This help page will guide you on how to use the app effectively.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;//To make the headings of each section stand out I decided to make them a different color
        Console.WriteLine("▶ MAIN NAVIGATION");
        Console.ResetColor();
        Console.WriteLine(" - Use the up and down arrows to select options from the menu.");
        Console.WriteLine(" - Follow on-screen prompts to view, insert, update, or delete records.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▶ VIEWING TABLES");
        Console.ResetColor();
        Console.WriteLine(" - Use ← and → arrow keys to move between pages when viewing long tables.");
        Console.WriteLine(" - If you are already on the first or last page, pressing arrows will keep you on the same page.");
        Console.WriteLine(" - Press any other key to return to the previous menu.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▶ DATA INPUT");
        Console.ResetColor();
        Console.WriteLine(" - When editing or adding records, follow the field prompts carefully.");
        Console.WriteLine(" - Use date format yyyy-mm-dd (e.g. 2025-07-08) and do not include units such as Kg");
        Console.WriteLine(" - To cancel, press do CTRL + B to go back, it will not be saved");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▶ USER VS ADMIN");
        Console.ResetColor();
        Console.WriteLine(" - There may be restrictions to the information made acessible for users");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▶ DISPLAY RECOMMENDATION");
        Console.ResetColor();
        Console.WriteLine(" - For best experience, run this app in FULL SCREEN.");
        Console.WriteLine(" - Some formatting may not align properly in small console windows.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▶ TROUBLESHOOTING");
        Console.ResetColor();
        Console.WriteLine(" - If you get stuck or an option seems unclear, return to the main menu or exit and try again.");
        Console.WriteLine(" - This app is designed with step-by-step guidance for each operation.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red; //This is red because its an important message 
        Console.WriteLine("Press any key to go back...");
        Console.ResetColor();
        Console.WriteLine();
    }

    //This method is made to be called in place of Console.Clear() so that my header and information is always at the top.
    public void Clear(string nameOfPage)
	{
		Console.Clear(); //First clears the page like console.clear would
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Sets console to UTF-8 so it can correctly use some of the characters that are in the title.

        string title = @"
███╗  ██╗██╗   ██╗████████╗██████╗ ██╗  ████████╗██████╗  █████╗  █████╗ ██╗  ██╗███████╗██████╗ 
████╗ ██║██║   ██║╚══██╔══╝██╔══██╗██║  ╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝██╔════╝██╔══██╗
██╔██╗██║██║   ██║   ██║   ██████╔╝██║     ██║   ██████╔╝███████║██║  ╚═╝█████═╝ █████╗  ██████╔╝
██║╚████║██║   ██║   ██║   ██╔══██╗██║     ██║   ██╔══██╗██╔══██║██║  ██╗██╔═██╗ ██╔══╝  ██╔══██╗
██║ ╚███║╚██████╔╝   ██║   ██║  ██║██║     ██║   ██║  ██║██║  ██║╚█████╔╝██║ ╚██╗███████╗██║  ██║
╚═╝  ╚══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝     ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝"; //This is a multiline string so it will display exactly as it is written here

        int consoleWidth = Console.WindowWidth; //To get the current width of the console winodw
        string[] eachLine = title.Split('\n'); //this splits the title into multiple lines, splitting by the '\n'
        foreach (string aLine in eachLine)
        {
            string trimmed = aLine.TrimEnd('\r'); // After splitting, the leftover lines may still have \r at the end (in windows) wich causes weird spacing so this line removes it. 
            int padding = (consoleWidth - trimmed.Length) / 2; //calculating horizontal padding to center it
            Console.SetCursorPosition(Math.Max(0, padding), Console.CursorTop); //now here it sets cursor to these new coordinated found
            Console.WriteLine(trimmed);
        }

		Console.WriteLine();
        Console.WriteLine("+" + new string('-', consoleWidth - 2) + "+"); //this is the first divider on top

        //these are the labels for my header
        string left = "[Back: Ctrl + B]";
        string right = "[Help: Ctrl + H] [Exit: Ctrl + E]";

        int y = Console.CursorTop; //this is to store the current vertical position taht is right now
        Console.SetCursorPosition(0, y); //now printing the left border of the header
        Console.Write("|");

        //writing the back: ctrl + b
        Console.SetCursorPosition(2, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(left);
        Console.ForegroundColor = ConsoleColor.White;

        //now if the name of the page taht is passed to this method is not too big then it is centered and written in between the labels
        if (nameOfPage.Length < consoleWidth - 2)
        {
            int center = (consoleWidth - nameOfPage.Length) / 2;
            Console.SetCursorPosition(center, y);
            Console.Write(nameOfPage);
        }
        else //otherwise theres just nothing written in the middle
        {
            int center = (consoleWidth - "".Length) / 2;
            Console.SetCursorPosition(center, y);
            Console.Write("");
        }

        //this is printing the right labels help and exit with a margin
        int rightMarg = consoleWidth - 2 - right.Length;
        Console.SetCursorPosition(rightMarg, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(right);
        Console.ForegroundColor = ConsoleColor.White;

        //the right border of the header
        Console.SetCursorPosition(consoleWidth - 1, y);
        Console.Write("|");

        //now it goes to the next line and draws the border under the header
        Console.SetCursorPosition(0, y + 1);
        Console.WriteLine("+" + new string('-', consoleWidth - 2) + "+");
    }
}