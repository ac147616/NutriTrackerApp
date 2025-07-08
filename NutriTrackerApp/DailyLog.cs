using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class DailyLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public int FoodID { get; set; }
        public string MealTime { get; set; }
        public string DateLogged { get; set; }

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
