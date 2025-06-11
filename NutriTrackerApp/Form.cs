using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class Form
    {
        private string[] fields; //This stores the names of the different fields that I want the user to enter their details into
        private string[] info; //This stores the information that the user's enter into the fields.
        private int currentField = 0; //This is basically the line that the user's cursor will be on

        public Form(string[] fields)
        {
            this.fields = fields;
            info = new string[fields.Length]; //tells how many responses are expected
            for (int i = 0; i < info.Length; i++) //this creates a placeholder for all the items that are going to be in this list
            {
                info[i] = "";
            }
        }
    }
}
