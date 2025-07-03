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
    public void UpdateUser(UserDetails user1)
    {
        string query = @"UPDATE users.tblUserDetails 
                    SET firstName = @FirstName, 
                        lastName = @LastName, 
                        emailID = @EmailID, 
                        passwordKey = @PasswordKey, 
                        age = @Age, 
                        gender = @Gender, 
                        userWeight = @UserWeight, 
                        userHeight = @UserHeight 
                    WHERE userID = @UserID";

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
            cmd.Parameters.AddWithValue("@UserID", user1.UserID);

            cmd.ExecuteNonQuery();
        }
    }
    public void PrintUserDetails(string userType, int? TheID)
    {
        Console.WriteLine();
        string query = (userType == "admin")
            ? "SELECT firstName, lastName, emailID, age, gender, userWeight, userHeight, signUpDate FROM users.tblUserDetails"
            : "SELECT firstName, lastName, emailID, age, gender, userWeight, userHeight, signUpDate FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (userType != "admin" && TheID.HasValue)
            {
                cmd.Parameters.AddWithValue("@UserID", TheID.Value);
            }

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("No records found.");
                    return;
                }

                var headers = new[]
                {
                ("First Name", 11),
                ("Last Name", 11),
                ("Email", 20),
                ("Age", 3),
                ("Gender", 6),
                ("Weight", 6),
                ("Height", 6),
                ("Date", 10)
            };

                foreach (var (title, width) in headers)
                {
                    Console.Write(cut(title, width) + " | ");
                }
                Console.WriteLine();
                int totalWidth = 11 + 11 + 20 + 3 + 6 + 6 + 6 + 10 + (3 * 8);
                Console.WriteLine(new string('-', totalWidth)); // separator

                int rowCount = 0;

                // Print each row
                while (reader.Read() && rowCount < 100)
                {
                    Console.Write(cut(reader.GetString(0), 11) + " | "); // First Name
                    Console.Write(cut(reader.GetString(1), 11) + " | "); // Last Name
                    Console.Write(cut(reader.GetString(2), 20) + " | "); // Email
                    Console.Write(cut(reader.GetInt32(3).ToString(), 3) + " | "); // Age
                    Console.Write(cut(reader.GetString(4), 6) + " | "); // Gender
                    Console.Write(cut(Math.Round(reader.GetDecimal(5)).ToString(), 6) + " | "); // Weight
                    Console.Write(cut(Math.Round(reader.GetDecimal(6)).ToString(), 6) + " | "); // Height
                    Console.Write(cut(Convert.ToDateTime(reader.GetDateTime(7)).ToString("yyyy-MM-dd"), 10));
                    Console.WriteLine();

                    rowCount++;
                }
            }
        }
    }
    private string cut(string value, int width)
    {
        if (value.Length < width)
        {
            string extraNeeded = new string(' ', width - value.Length);
            value = extraNeeded + value;
            return value;
        }
        if (value.Length > width)
        {
            return value.Substring(0, Math.Max(0, width - 3)) + "...";
        }
        return value.PadRight(width);
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
	public int GetUserID(int userID, string passwordkey)
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
				return 0;
			}
        }
    }
    public UserDetails GetUserByID(int id)
    {
        string query = "SELECT * FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new UserDetails(
                        id,
                        reader.GetString(1), // FirstName
                        reader.GetString(2), // LastName
                        reader.GetString(3), // Email
                        reader.GetString(4), // Password
                        reader.GetInt32(5),  // Age
                        reader.GetString(6), // Gender
                        Convert.ToDouble(reader.GetDecimal(7)), // Weight
                        Convert.ToDouble(reader.GetDecimal(8)), // Height
                        reader.GetDateTime(9).ToString("yyyy-MM-dd") // SignUpDate
                    );
                }
            }
        }
        return null;
    }
    public int GetAdminID(int adminID, string passwordkey)
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
                return 0;
            }
        }
    }

}