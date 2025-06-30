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

	public int? GetUserID(int userID, string passwordkey)
	{
        using (SqlCommand cmd = new SqlCommand("SELECT userID FROM UserDetails WHERE userID = @userID AND passwordkey = @passwordkey", conn))
        {
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@passwordkey", passwordkey);

			if (cmd.ExecuteScalar() != null)
			{ 
				return Convert.ToInt32(cmd.ExecuteScalar()); //return only one column
			}
			else
			{
				return null;
			}
        }
    }

    public List<UserDetails> GetAllUserDetails()
    {
        List<UserDetails> users = new List<UserDetails>();

        string query = "SELECT * FROM users.tblUserDetails"; // Adjust schema/table if needed

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userID = Convert.ToInt32(reader["userID"]);
                    string firstName = reader["firstName"].ToString();
                    string lastName = reader["lastName"].ToString();
                    string emailID = reader["emailID"].ToString();
                    string passwordKey = reader["passwordKey"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    string gender = reader["gender"].ToString();
                    double weight = Convert.ToDouble(reader["userWeight"]);
                    double height = Convert.ToDouble(reader["userHeight"]);
                    DateOnly signUpDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["signUpDate"]));

                    users.Add(new UserDetails(userID, firstName, lastName, emailID, passwordKey, age, gender, weight, height, signUpDate));
                }
            }
        }

        return users;
    }

}