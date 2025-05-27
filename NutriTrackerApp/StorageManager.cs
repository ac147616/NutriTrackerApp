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
			Console.WriteLine("Connection Successful");
		}
		catch (SqlException e)
		{
			Console.WriteLine("Connection NOT Successful\n");
			Console.WriteLine(e.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred");
			Console.WriteLine(ex.Message);
		}
	}

	public List<Food> GetAllFoods()
	{
		List<Food> foods = new List<Food>();
		string sqlString = "SELECT * FROM admin.Foods";
		using (SqlCommand cmd = new SqlCommand(sqlString, conn))
		{
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					int foodId = Convert.ToInt32(reader["FOOD_ID"]);
					string foodName = reader["FOOD_NAME"].ToString();
					foods.Add(new Food(foodId, foodName));
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

	public int DeleteFoodByName(string foodName)
	{
		using (SqlCommand cmd = new SqlCommand("DELETE FROM admin.Foods WHERE FOOD_NAME = @FoodName", conn))
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
}