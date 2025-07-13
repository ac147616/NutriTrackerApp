using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    //This is my class for the table admins.tblDietPlans
    public class DietPlans
    {
        public int DietPlanID { get; set; }
        public string DietPlan { get; set; }
        public int CaloriesTarget { get; set; }
        public int ProteinsTarget { get; set; }
        public int CarbohydratesTarget { get; set; }
        public int FatsTarget { get; set; }

        public DietPlans(int dietPlanID, string dietPlan, int caloriesTarget, int proteinsTarget, int carbohydratesTarget, int fatsTarget)
        {
            DietPlanID = dietPlanID;
            DietPlan = dietPlan;
            CaloriesTarget = caloriesTarget;
            ProteinsTarget = proteinsTarget;
            CarbohydratesTarget = carbohydratesTarget;
            FatsTarget = fatsTarget;
        }
    }
}
