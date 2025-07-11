using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is my class for the table users.tblAllergies
namespace NutriTrackerApp
{
    public class Allergies 
    {
        public int AllergyID { get; set; } 
        public int UserID { get; set; } //This is a fk reference in the table to users.tblUserDetails
        public string Allergy { get; set; } 

        public Allergies(int allergyID, int userID, string allergy)
        {
            AllergyID = allergyID;
            UserID = userID;
            Allergy = allergy;
        }
    }
}
