using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
   public class Foods
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Category { get; set; }
        public double Calories { get; set; }
        public double Carbohydrates { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double ServingSize { get; set; }
        public Foods(int foodID, string foodName, string category, double calories, double carbohydrates, double proteins, double fats, double servingSize)
        {
            FoodID = foodID;
            FoodName = foodName;
            Category = category;
            Calories = calories;
            Carbohydrates = carbohydrates;
            Proteins = proteins;
            Fats = fats;
            ServingSize = servingSize;

        }
    }
}
