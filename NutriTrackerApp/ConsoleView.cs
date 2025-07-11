using NutriTrackerApp;
using System;


public class ConsoleView
{
	public void Help()
	{
        Clear("Help");

        Console.WriteLine();
        Console.WriteLine("Welcome to NutriTrackerApp! This help page will guide you on how to use the app effectively.");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
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

        int y = Console.CursorTop;
        Console.SetCursorPosition(0, y);
        Console.Write("|");
        Console.SetCursorPosition(2, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(left);
        Console.ForegroundColor = ConsoleColor.White;

        if (nameOfPage.Length < consoleWidth - 2)
        {
            int center = (consoleWidth - nameOfPage.Length) / 2;
            Console.SetCursorPosition(center, y);
            Console.Write(nameOfPage);
        }
        else
        {
            int center = (consoleWidth - "".Length) / 2;
            Console.SetCursorPosition(center, y);
            Console.Write("");
        }

        int rightMarg = consoleWidth - 2 - right.Length;
        Console.SetCursorPosition(rightMarg, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(right);
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(consoleWidth - 1, y);
        Console.Write("|");

        Console.SetCursorPosition(0, y + 1);
        Console.WriteLine("+" + new string('-', consoleWidth - 2) + "+");
    }
}