using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
   public class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }

        public Food(int foodId, string foodName)
        {
            FoodID = foodId;
            FoodName = foodName;
        }
    }
}
