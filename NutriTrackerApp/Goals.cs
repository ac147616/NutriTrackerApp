using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    class Goals
    {
        public int GoalID { get; set; }
        public int UserID { get; set; }
        public int DietPlanID { get; set; }
        public string Goal { get; set; }
        public string DateStarted { get; set; }
        public string DateEnded { get; set; }

        public Goals(int goalID, int userID, int dietPlanID, string goal, string dateStarted, string dateEnded)
        {
            GoalID = goalID;
            UserID = userID;
            DietPlanID = dietPlanID;
            Goal = goal;
            DateStarted = dateStarted;
            DateEnded = dateEnded;
        }
    }
}
