using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    //This is my class for the table users.tblGoals
    public class Goals
    {
        public int GoalID { get; set; } 
        public int UserID { get; set; } //This is a fk reference to the ID field in table users.tblUserDetails
        public int DietPlanID { get; set; } //This is a fk reference to the ID field in table admins.tblDietPlans
        public string Goal { get; set; }
        public string DateStarted { get; set; } //Both these date fields are stored as strings because ist easier to get input this way, later when they are used in the DB they are converted to Date.
        public string? DateEnded { get; set; } //This field is allowed to be null because if you start a goal you don't know when you will finish it

        public Goals(int goalID, int userID, int dietPlanID, string goal, string dateStarted, string? dateEnded)
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
