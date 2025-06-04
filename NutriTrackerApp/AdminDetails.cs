using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class AdminDetails
    {
        public int AdminID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Passwordkey { get; set; }
        public AdminDetails(int adminID, string firstName, string lastName, string emailID, string passwordkey)
        {
            AdminID = adminID;
            FirstName = firstName;
            LastName = lastName;
            EmailID = emailID;
            Passwordkey = passwordkey;
        }
    }
}
