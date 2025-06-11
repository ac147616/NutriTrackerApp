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
            int startLine = Console.CursorTop; //So it starts on the first field

            while (true) //So this runs forever until the responses are collected
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
            }
        }

        public string[] ReturnAllInformation()
        {
            return Info;
        }
    }
}
