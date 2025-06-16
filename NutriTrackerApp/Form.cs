using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class Form
    {
        public string[] Fields { get; set; }
        public string[] Info { get; set; }
        public int CurrentField { get; set; }

        public Form(string[] fields)
        {
            Fields = fields;
            Info = new string[fields.Length]; // Initialize info array
            for (int i = 0; i < Info.Length; i++)
            {
                Info[i] = ""; // Set default empty strings
            }
            CurrentField = 0; // Set initial current field
        }
        public void DisplayForm()
        {
            int startLine = Console.CursorTop;

            while (true)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    Console.SetCursorPosition(0, startLine+1);
                    Console.Write(Fields[i]);
                    Console.Write(" ");
                    if (i == CurrentField)
                    {
                        Console.Write(Info[i]);
                    }
                    else
                    {
                        Console.Write(new string(' ', Info[i].Length));
                    }
                }

                Console.SetCursorPosition(Fields[CurrentField].Length + 1 + Info.Length, startLine + CurrentField);
                var keyType = Console.ReadKey(true);

                switch (keyType.Key)
                {
                    case ConsoleKey.Enter:
                        if (CurrentField == Info.Length - 1)
                        {
                            return;
                        }
                        else
                        {
                            CurrentField++;
                        }                            
                        break;
                    case ConsoleKey.UpArrow:
                        if (CurrentField > 0)
                        {
                            CurrentField--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (CurrentField < Fields.Length - 1)
                        {
                            CurrentField++;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (Info[CurrentField].Length > 0)
                        {
                            Info[CurrentField] = Info[CurrentField].Substring(0, Info[CurrentField].Length -1);
                        }
                        break;
                }
            }//Um So i already have seperate admina dn suer tables as part of my database so i didnt have to make a new like users tale. But because i used first name and last name as fields and my unique ID was just like numbers instead of username or emailID im having to use
        }

        public string[] ReturnAllInformation()
        {
            return Info;
        }
    }
}
