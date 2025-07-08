using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class Allergies 
    {
        public int AllergyID { get; set; } 
        public int UserID { get; set; } 
        public string Allergy { get; set; } 

        public Allergies(int allergyID, int userID, string allergy)
        {
            AllergyID = allergyID;
            UserID = userID;
            Allergy = allergy;
        }
    }
}
