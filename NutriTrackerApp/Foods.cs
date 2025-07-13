using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    //This is my class for the table admins.tblFoods
   public class Foods
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Category { get; set; }
        public decimal Calories { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Proteins { get; set; }
        public decimal Fats { get; set; }
        public decimal ServingSize { get; set; }
        public Foods(int foodID, string foodName, string category, decimal calories, decimal carbohydrates, decimal proteins, decimal fats, decimal servingSize)
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
