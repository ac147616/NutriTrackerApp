using Microsoft.Data.SqlClient;
using NutriTrackerApp;
using System;
using System.Data;

public class StorageManager
{
	private SqlConnection conn;

	public StorageManager(string connectionString)
	{
		try
		{
			conn = new SqlConnection(connectionString);
			conn.Open();
			Console.WriteLine("Your connection was successful!");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\nLoading interface...");
        }
		catch (SqlException e)
		{
			Console.WriteLine("Your connection was unsuccessful...\n");
			Console.WriteLine(e.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred...");
			Console.WriteLine(ex.Message);
		}
	}

	public List<Food> GetAllFoods()
	{
		List<Food> foods = new List<Food>();
		string sqlString = "SELECT * FROM admin.tblFoods";
		using (SqlCommand cmd = new SqlCommand(sqlString, conn))
		{
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					int foodId = Convert.ToInt32(reader["FOOD_ID"]);
					string foodName = reader["FOOD_NAME"].ToString();
					string category = reader["CATEGORY"].ToString();
                    double calories = Convert.ToDouble(reader["CATEGORY"].ToString());
                    double carbohydrates = Convert.ToDouble(reader["CARBOHYDRATES"].ToString());
                    double proteins = Convert.ToDouble(reader["PROTEINS"].ToString());
                    double fats = Convert.ToDouble(reader["FATS"].ToString());
                    double servingSize = Convert.ToDouble(reader["SERVING_SIZE"].ToString());
                    foods.Add(new Food(foodId, foodName, category, calories, carbohydrates, proteins, fats, servingSize));
				}
			}
		}
		return foods;
	}

	public int InsertFood(Food foodtemp)
	{
		using (SqlCommand cmd = new SqlCommand("INSERT INTO admin.Foods (FOOD_NAME) VALUES (@FoodName); SELECT SCOPE_IDENTITY();", conn))
		{
			cmd.Parameters.AddWithValue("@FoodName", foodtemp.FoodName);
			return Convert.ToInt32(cmd.ExecuteScalar());
		}

    }

    public int InsertUserDetails(UserDetails userDetailstemp)
    {
        using (SqlCommand cmd = new SqlCommand("INSERT INTO users.UserDetails (firstName) VALUES (@FoodName); SELECT SCOPE_IDENTITY();", conn))
        {
            cmd.Parameters.AddWithValue("@FoodName", userDetailstemp.FirstName);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

    }

    public int DeleteFoodByName(string foodName)
	{
		using (SqlCommand cmd = new SqlCommand("DELETE FROM admin.Foods WHERE FOOD_NAME = @FoodName", conn))
		{
			cmd.Parameters.AddWithValue("@FoodName", foodName);
			return cmd.ExecuteNonQuery();
		}

	}

	public int UpdateFoodName(int foodID, string foodName)
	{
        using (SqlCommand cmd = new SqlCommand("UPDATE admin.Foods SET FOOD_NAME = @FoodName WHERE FOOD_ID = foodID", conn))
        {
            cmd.Parameters.AddWithValue("@FoodName", foodName);
            return cmd.ExecuteNonQuery();
        }
    }
	public void CloseConnection()
	{
		if (conn != null && conn.State == ConnectionState.Open)
		{
			conn.Close();
			Console.WriteLine("Connection closed");
		}
	}

	//validates if a user exists..the question mark means that it can also be null
	/*public int? CheckUser(int userID, string password)
	{	

		return;
	} */

	}