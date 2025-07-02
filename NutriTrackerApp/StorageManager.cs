using Azure;
using Microsoft.Data.SqlClient;
using NutriTrackerApp;
using System;
using System.Data;
using System.Reflection;

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

	public int InsertUser(UserDetails user1)
	{
        string query = "INSERT INTO users.tblUserDetails (firstName, lastName, emailID, passwordKey, age, gender, userWeight, userHeight, signUpDate) VALUES (@FirstName, @LastName, @EmailID, @PasswordKey, @Age, @Gender, @UserWeight, @UserHeight, @SignUpDate); SELECT SCOPE_IDENTITY();";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", user1.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user1.LastName);
            cmd.Parameters.AddWithValue("@EmailID", user1.EmailID);
            cmd.Parameters.AddWithValue("@PasswordKey", user1.Passwordkey);
            cmd.Parameters.AddWithValue("@Age", user1.Age);
            cmd.Parameters.AddWithValue("@Gender", user1.Gender);
            cmd.Parameters.AddWithValue("@UserWeight", user1.UserWeight);
            cmd.Parameters.AddWithValue("@UserHeight", user1.UserHeight);
            cmd.Parameters.AddWithValue("@SignUpDate", user1.SignUpDate);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public void PrintLatestUserDetails()
    {
        string query = "SELECT TOP 1 * FROM users.tblUserDetails ORDER BY userID DESC";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Console.WriteLine("----- Latest User Details -----");
                Console.WriteLine($"User ID   : {reader["userID"]}");
                Console.WriteLine($"First Name: {reader["firstName"]}");
                Console.WriteLine($"Last Name : {reader["lastName"]}");
                Console.WriteLine($"Email     : {reader["emailID"]}");
                // Add more fields as needed
            }
            else
            {
                Console.WriteLine("No users found in the database.");
            }
        }
    }

    public List<Food> GetAllFoods()
	{
		List<Food> foods = new List<Food>();
		string sqlString = "SELECT * FROM admins.tblFoods";
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
        using (SqlCommand cmd = new SqlCommand("SELECT userID FROM users.tblUserDetails WHERE userID = @userID AND passwordkey = @passwordkey", conn))
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
    public int? GetAdminID(int adminID, string passwordkey)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT adminID FROM admins.tblAdminDetails WHERE adminID = @adminID AND passwordkey = @passwordkey", conn))
        {
            cmd.Parameters.AddWithValue("@adminID", adminID);
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

}