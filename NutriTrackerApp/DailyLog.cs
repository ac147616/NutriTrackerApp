using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is my class for the table users.tblDailyLog
namespace NutriTrackerApp
{
    public class DailyLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; } //This is a fk reference to the ID field in users.tblUserDetails
        public int FoodID { get; set; } //This is a fk reference to the ID field in admins.tblFoods
        public string MealTime { get; set; }
        public string DateLogged { get; set; } //It is stored as a string to make getting input easier but later when it is passed into the DB it is converted to Date

        public DailyLog(int logID, int userID, int foodID, string mealTime, string dateLogged)
        {
            LogID = logID;
            UserID = userID;
            FoodID = foodID;
            MealTime = mealTime;
            DateLogged = dateLogged;
        }
    }
}
