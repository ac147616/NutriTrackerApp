using Azure;
using Microsoft.Data.SqlClient;
using NutriTrackerApp;
using System;
using System.Data;
using System.Reflection;

public class StorageManager
{
    public SqlConnection conn;
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
    public int InsertAdmin(AdminDetails admin)
    {
        string query = "INSERT INTO admins.tblAdminDetails (firstName, lastName, emailID, passwordKey) " +
                       "VALUES (@FirstName, @LastName, @EmailID, @PasswordKey); SELECT SCOPE_IDENTITY();";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
            cmd.Parameters.AddWithValue("@LastName", admin.LastName);
            cmd.Parameters.AddWithValue("@EmailID", admin.EmailID);
            cmd.Parameters.AddWithValue("@PasswordKey", admin.Passwordkey);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    public bool InsertAllergy(int userID, string allergy)
    {
        string query = "INSERT INTO users.tblAllergies (userID, allergy) VALUES (@UserID, @Allergy)";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Allergy", allergy);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // returns true if insert was successful
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error inserting allergy: " + ex.Message);
                return false;
            }
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
    public void UpdateAdmin(AdminDetails admin)
    {
        string query = @"UPDATE admins.tblAdminDetails 
                     SET firstName = @FirstName, 
                         lastName = @LastName, 
                         emailID = @EmailID, 
                         passwordKey = @PasswordKey 
                     WHERE adminID = @AdminID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
            cmd.Parameters.AddWithValue("@LastName", admin.LastName);
            cmd.Parameters.AddWithValue("@EmailID", admin.EmailID);
            cmd.Parameters.AddWithValue("@PasswordKey", admin.Passwordkey);
            cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);

            cmd.ExecuteNonQuery();
        }
    }
    public void PrintUserDetails2(string userType, int? TheID)
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
    public void PrintUserDetails(string userType, int? TheID)
    {
        ConsoleView view = new ConsoleView();
        List<string[]> userList = new List<string[]>();

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
                while (reader.Read())
                {
                    string[] row = new string[8];
                    row[0] = reader.GetString(0); // First Name
                    row[1] = reader.GetString(1); // Last Name
                    row[2] = reader.GetString(2); // Email
                    row[3] = reader.GetInt32(3).ToString(); // Age
                    row[4] = reader.GetString(4); // Gender
                    row[5] = Math.Round(reader.GetDecimal(5)).ToString(); // Weight
                    row[6] = Math.Round(reader.GetDecimal(6)).ToString(); // Height
                    row[7] = reader.GetDateTime(7).ToString("yyyy-MM-dd"); // Date
                    userList.Add(row);
                }
            }
        }

        if (userList.Count == 0)
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

        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("View User Details"); // Optional title parameter omitted since no heading is needed

            // Prepare header
            string columnHeader = string.Join(" | ", headers.Select(h => cut(h.Item1, h.Item2)));
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            // Calculate paging
            int totalPages = (int)Math.Ceiling((double)userList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, userList.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                string line = string.Join(" | ", new[]
                {
                cut(userList[i][0], 11),
                cut(userList[i][1], 11),
                cut(userList[i][2], 20),
                cut(userList[i][3], 3),
                cut(userList[i][4], 6),
                cut(userList[i][5], 6),
                cut(userList[i][6], 6),
                cut(userList[i][7], 10)
            });

                Console.WriteLine(pad + line);
            }

            Console.WriteLine(pad + new string('-', tableWidth));

            // For admin: show page info + scroll logic
            if (userType == "admin")
            {
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(pad + "Press any other key to go back");
                Console.ResetColor();

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow && totalPages > 1 && currentPage > 0)
                {
                    currentPage--;
                }
                else if (key.Key == ConsoleKey.RightArrow && totalPages > 1 && currentPage < totalPages - 1)
                {
                    currentPage++;
                }
                else if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
                {
                    break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(pad + "Press any other key to go back");
                Console.ResetColor();
                Console.ReadKey(true);
                break;
            }
        }
    }
    public void ViewAllAdmins()
    {
        ConsoleView view = new ConsoleView();
        List<AdminDetails> adminList = new List<AdminDetails>();

        string query = "SELECT adminID, firstName, lastName, emailID, passwordkey FROM admins.tblAdminDetails";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                AdminDetails admin = new AdminDetails(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
                adminList.Add(admin);
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("View All Admins");
            Console.WriteLine();

            string columnHeader = string.Format("{0,-6}    {1,-15}    {2,-15}    {3,-25}", "ID", "First Name", "Last Name", "Email");
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad); // apply color to padding too
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            int totalPages = (int)Math.Ceiling((double)adminList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, adminList.Count);

            if (adminList.Count == 0)
            {
                Console.WriteLine(pad + "No admin records found.");
            }
            else
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    AdminDetails admin = adminList[i];
                    string line = string.Format("{0,-6}    {1,-15}    {2,-15}    {3,-25}",
                        admin.AdminID,
                        Truncate(admin.FirstName, 15),
                        Truncate(admin.LastName, 15),
                        Truncate(admin.EmailID, 25)
                    );
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to go back");
            Console.ResetColor();

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow && totalPages > 1 && currentPage > 0)
            {
                currentPage--;
            }
            else if (key.Key == ConsoleKey.RightArrow && totalPages > 1 && currentPage < totalPages - 1)
            {
                currentPage++;
            }
            else if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
            {
                break;
            }
            // If left/right pressed but not valid → stay on same page
        }
    }
    public void ViewAllAllergies(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<(string Allergy, int AllergyID)> allergyList = new List<(string, int)>();

        string query = "SELECT allergy, allergyID FROM users.tblAllergies WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string allergy = reader.GetString(0);
                    int allergyID = reader.GetInt32(1);
                    allergyList.Add((allergy, allergyID));
                }
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("View All Alergies");

            // Center the heading
            string heading = $"Allergies for User ID: {userID}";
            int headingPad = Math.Max(0, (consoleWidth - heading.Length) / 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string(' ', headingPad) + heading);
            Console.ResetColor();
            Console.WriteLine();

            // Header
            string columnHeader = string.Format("{0,-5}    {1,-30}", "ID", "Allergy"); // 4 spaces
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);

            Console.WriteLine(new string(' ', leftPad) + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string(' ', leftPad)); // Set color before writing padding
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(new string(' ', leftPad) + new string('-', tableWidth));

            // Paging
            int totalPages = (int)Math.Ceiling((double)allergyList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, allergyList.Count);

            if (allergyList.Count == 0)
            {
                Console.WriteLine(new string(' ', leftPad) + "No allergies recorded.");
            }
            else
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    var allergy = allergyList[i];
                    string line = string.Format("{0,-5}    {1,-30}", allergy.AllergyID, Truncate(allergy.Allergy, 30));
                    Console.WriteLine(new string(' ', leftPad) + line);
                }

                Console.WriteLine(new string(' ', leftPad) + new string('-', tableWidth));
                Console.WriteLine(new string(' ', leftPad) + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine(new string(' ', leftPad) + "Press any other key to go back");
            Console.ResetColor();

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow && totalPages > 1 && currentPage > 0)
            {
                currentPage--;
            }
            else if (key.Key == ConsoleKey.RightArrow && totalPages > 1 && currentPage < totalPages - 1)
            {
                currentPage++;
            }
            else if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
            {
                break; // Any other key breaks regardless of page count
            }
            // If it's an arrow but page cannot move, do nothing (stay on same page)

        }
    }
    private string Truncate(string text, int maxLength)
    {
        if (text.Length <= maxLength)
            return text;
        return text.Substring(0, maxLength - 1) + "…";
    }
    public void PrintAllergiesByUserID(int userID)
    {
        string query = "SELECT allergy, allergyID FROM users.tblAllergies WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Allergies for User ID: {userID}");
                Console.ResetColor();
                Console.WriteLine(new string('-', 40));

                int count = 0;

                while (reader.Read())
                {
                    string allergy = reader.GetString(0);
                    string allergyID = Convert.ToString(reader.GetInt32(1));
                    Console.WriteLine($"- {allergy} (ID: {allergyID})");
                    count++;
                }

                if (count == 0)
                {
                    Console.WriteLine("No allergies recorded.");
                }

                Console.WriteLine(new string('-', 40));
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
    public bool DeleteUserByID(int userID)
    {
        string query = "DELETE FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    public bool DeleteAdminByID(int adminID)
    {
        string query = "DELETE FROM admins.tblAdminDetails WHERE adminID = @AdminID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@AdminID", adminID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    public bool DeleteAllergyByID(int allergyID, int userID)
    {
        string query = @"DELETE FROM users.tblAllergies 
                     WHERE allergyID = @AllergyID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@AllergyID", allergyID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
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
    public AdminDetails GetAdminByID(int id)
    {
        string query = "SELECT * FROM admins.tblAdminDetails WHERE adminID = @AdminID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@AdminID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new AdminDetails(
                        id,
                        reader.GetString(1), // FirstName
                        reader.GetString(2), // LastName
                        reader.GetString(3), // EmailID
                        reader.GetString(4)  // PasswordKey
                    );
                }
            }
        }
        return null;
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
    public void CloseConnection()
    {
        if (conn != null && conn.State == ConnectionState.Open)
        {
            conn.Close();
            Console.WriteLine("Connection closed");
        }
    }
}