using Microsoft.Data.SqlClient;
using NutriTrackerApp;
using System.Data;

public class StorageManager
{
    public SqlConnection conn; //declaring the connection for the SQL Server databse 
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
    //inserts a new user into the tblUserDetails table and returns the newly generated user ID.
    public int InsertUser(UserDetails user1)
    {
        //defines an SQL query with parameter placeholders and returns the ID of the inserted row.
        string query = "INSERT INTO users.tblUserDetails (firstName, lastName, emailID, passwordKey, age, gender, userWeight, userHeight, signUpDate) VALUES (@FirstName, @LastName, @EmailID, @PasswordKey, @Age, @Gender, @UserWeight, @UserHeight, @SignUpDate); SELECT SCOPE_IDENTITY();";

        //links each property from the UserDetails object to the related SQL parameter.
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

            //executes the insert command and returns the newly created ID as an integer.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
  //this method inserts a new admin tecord in the DB and returns the ID of the new record created.
    public int InsertAdmin(AdminDetails admin)
    {
        //the sql query to insert admin details in table...SCOPE_IDENTITY() gets the ID of the inserted row.
        string query = "INSERT INTO admins.tblAdminDetails (firstName, lastName, emailID, passwordKey) " +
                       "VALUES (@FirstName, @LastName, @EmailID, @PasswordKey); SELECT SCOPE_IDENTITY();";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from AdminDetails object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
            cmd.Parameters.AddWithValue("@LastName", admin.LastName);
            cmd.Parameters.AddWithValue("@EmailID", admin.EmailID);
            cmd.Parameters.AddWithValue("@PasswordKey", admin.Passwordkey);

            //ExecuteScalar() returns the first column of the result and then its converted to a no.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    //this method attempts to insert a new allergy record in the DB and returns whether it was successful in boolean form.
    public bool InsertAllergy(int userID, string allergy)
    {
        //the sql query to insert allergy in table
        string query = "INSERT INTO users.tblAllergies (userID, allergy) VALUES (@UserID, @Allergy)";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from Allerhies object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Allergy", allergy);

            try
            {   //ExecuteNonQuery returns the number of rows affected.
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // returns true if insert was successful
            }
            catch (SqlException ex)
            {
                //if there was an error related to SQL, an error message is printed to the conosle, also explaining what the error was.
                Console.WriteLine("Error inserting allergy: " + ex.Message);
                return false;
            }
        }
    }
    //this method inserts a new food record in the DB and returns the ID of the new record created.
    public int InsertFood(Foods food)
    {
        //the sql query to insert food in table...SCOPE_IDENTITY() gets the ID of the inserted row.
        string query = @"
        INSERT INTO admins.tblFoods 
        (foodName, category, calories, carbohydrates, proteins, fats, servingSize) 
        VALUES 
        (@FoodName, @Category, @Calories, @Carbohydrates, @Proteins, @Fats, @ServingSize);
        SELECT SCOPE_IDENTITY();";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from foods object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
            cmd.Parameters.AddWithValue("@Category", food.Category);
            cmd.Parameters.AddWithValue("@Calories", food.Calories);
            cmd.Parameters.AddWithValue("@Carbohydrates", food.Carbohydrates);
            cmd.Parameters.AddWithValue("@Proteins", food.Proteins);
            cmd.Parameters.AddWithValue("@Fats", food.Fats);
            cmd.Parameters.AddWithValue("@ServingSize", food.ServingSize);

            //ExecuteScalar() returns the first column of the result and then its converted to a no.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    //this method inserts a new diet plan record in the DB and returns the ID of the new record created.
    public int InsertDietPlan(DietPlans plan)
    {
        //the sql query to insert diet plan in table...SCOPE_IDENTITY() gets the ID of the inserted row.
        string query = @"
        INSERT INTO admins.tblDietPlans 
        (dietPlan, caloriesTarget, proteinsTarget, carbohydratesTarget, fatsTarget) 
        VALUES 
        (@DietPlan, @CaloriesTarget, @ProteinsTarget, @CarbohydratesTarget, @FatsTarget);
        SELECT SCOPE_IDENTITY();";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from DietPlan object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@DietPlan", plan.DietPlan);
            cmd.Parameters.AddWithValue("@CaloriesTarget", plan.CaloriesTarget);
            cmd.Parameters.AddWithValue("@ProteinsTarget", plan.ProteinsTarget);
            cmd.Parameters.AddWithValue("@CarbohydratesTarget", plan.CarbohydratesTarget);
            cmd.Parameters.AddWithValue("@FatsTarget", plan.FatsTarget);

            //ExecuteScalar() returns the first column of the result and then its converted to a no.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    //this method inserts a new goal record in the DB and returns the ID of the new record created.
    public int InsertGoal(Goals goal)
    {
        //the sql query to insert goal in table...SCOPE_IDENTITY() gets the ID of the inserted row.
        string query = @"
        INSERT INTO users.tblGoals 
        (userID, dietPlanID, goal, dateStarted, dateEnded) 
        VALUES 
        (@UserID, @DietPlanID, @Goal, @DateStarted, @DateEnded);
        SELECT SCOPE_IDENTITY();";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from Goal object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@UserID", goal.UserID);
            cmd.Parameters.AddWithValue("@DietPlanID", goal.DietPlanID);
            cmd.Parameters.AddWithValue("@Goal", goal.Goal);
            cmd.Parameters.AddWithValue("@DateStarted", goal.DateStarted);

            if (goal.DateEnded == null)
                cmd.Parameters.AddWithValue("@DateEnded", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@DateEnded", goal.DateEnded);

            //ExecuteScalar() returns the first column of the result and then its converted to a no.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    //this method inserts a new daily log record in the DB and returns the ID of the new record created.
    public int InsertDailyLog(DailyLog log)
    {
        //the sql query to insert daily log in table...SCOPE_IDENTITY() gets the ID of the inserted row.
        string query = @"
    INSERT INTO users.tblDailyLog 
    (userID, foodID, mealTime, dateLogged) 
    VALUES 
    (@UserID, @FoodID, @MealTime, @DateLogged);
    SELECT SCOPE_IDENTITY();";

        //creates new sql command using the query above.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //linking values from DailyLog object by creating parameters for the SQL command.
            cmd.Parameters.AddWithValue("@UserID", log.UserID);
            cmd.Parameters.AddWithValue("@FoodID", log.FoodID);
            cmd.Parameters.AddWithValue("@MealTime", log.MealTime);
            cmd.Parameters.AddWithValue("@DateLogged", log.DateLogged);

            //ExecuteScalar() returns the first column of the result and then its converted to a no.
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    
    //this method updates an existing user's details in the 'users.tblUserDetails' table.
    public void UpdateUser(UserDetails user1)
    {
        //this is a slq query statement with paramters. This updates all user fields where the userID matches the one of the current user.
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

        //add values to the SQL parameters from the passed UserDetails object. This also binds the actual user input to the query to prevent SQL injection.
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

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            cmd.ExecuteNonQuery();
        }
    }
    //this method updates an existing admin's details in the 'admins.tblAdminDetails' table.
    public void UpdateAdmin(AdminDetails admin)
    {
        //this is a slq query statement with paramters. This updates all admin fields where the admin matches the one of the chose admin.
        string query = @"UPDATE admins.tblAdminDetails 
                     SET firstName = @FirstName, 
                         lastName = @LastName, 
                         emailID = @EmailID, 
                         passwordKey = @PasswordKey 
                     WHERE adminID = @AdminID";

        //add values to the SQL parameters from the passed AdminDetails object. This also binds the actual input to the query to prevent SQL injection.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
            cmd.Parameters.AddWithValue("@LastName", admin.LastName);
            cmd.Parameters.AddWithValue("@EmailID", admin.EmailID);
            cmd.Parameters.AddWithValue("@PasswordKey", admin.Passwordkey);
            cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            cmd.ExecuteNonQuery();
        }
    }
    //this method updates an existing food's details in the 'admins.tblFoods' table.
    public bool UpdateFood(Foods food)
    {
        //this is a slq query statement with paramters. This updates all food fields where the foodID matches the one of the selected food.
        string query = @"UPDATE admins.tblFoods 
                     SET foodName = @FoodName, 
                         category = @Category, 
                         calories = @Calories, 
                         carbohydrates = @Carbohydrates, 
                         proteins = @Proteins, 
                         fats = @Fats, 
                         servingSize = @ServingSize 
                     WHERE foodID = @FoodID";

        //add values to the SQL parameters from the passed Food object. This also binds the actual input to the query to prevent SQL injection.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
            cmd.Parameters.AddWithValue("@Category", food.Category);
            cmd.Parameters.AddWithValue("@Calories", food.Calories);
            cmd.Parameters.AddWithValue("@Carbohydrates", food.Carbohydrates);
            cmd.Parameters.AddWithValue("@Proteins", food.Proteins);
            cmd.Parameters.AddWithValue("@Fats", food.Fats);
            cmd.Parameters.AddWithValue("@ServingSize", food.ServingSize);
            cmd.Parameters.AddWithValue("@FoodID", food.FoodID);

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    //this method updates an existing adiet plan's details in the 'admins.tblDietPlan' table.
    public void UpdateDietPlan(DietPlans plan)
    {
        //this is a slq query statement with paramters. This updates all diet plan fields where the dietPlanID matches the one of the sekected diet plan.
        string query = @"UPDATE admins.tblDietPlans
                     SET dietPlan = @DietPlan,
                         caloriesTarget = @CaloriesTarget,
                         proteinsTarget = @ProteinsTarget,
                         carbohydratesTarget = @CarbohydratesTarget,
                         fatsTarget = @FatsTarget
                     WHERE dietPlanID = @DietPlanID";

        //add values to the SQL parameters from the passed DietPlans object. This also binds the actual input to the query to prevent SQL injection.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@DietPlan", plan.DietPlan);
            cmd.Parameters.AddWithValue("@CaloriesTarget", plan.CaloriesTarget);
            cmd.Parameters.AddWithValue("@ProteinsTarget", plan.ProteinsTarget);
            cmd.Parameters.AddWithValue("@CarbohydratesTarget", plan.CarbohydratesTarget);
            cmd.Parameters.AddWithValue("@FatsTarget", plan.FatsTarget);
            cmd.Parameters.AddWithValue("@DietPlanID", plan.DietPlanID);

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            cmd.ExecuteNonQuery();
        }
    }
    //this method updates an existing goal's details in the 'users.tblGoal' table.
    public void UpdateGoal(Goals goal)
    {
        //this is a slq query statement with paramters. This updates all goal fields where the goalID matches the one of the sekected goal.
        string query = @"UPDATE users.tblGoals 
                     SET dietPlanID = @DietPlanID, 
                         goal = @Goal,
                         dateEnded = @DateEnded
                     WHERE goalID = @GoalID AND userID = @UserID";

        //add values to the SQL parameters from the passed Goal object. This also binds the actual input to the query to prevent SQL injection.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@DietPlanID", goal.DietPlanID);
            cmd.Parameters.AddWithValue("@Goal", goal.Goal);
            cmd.Parameters.AddWithValue("@GoalID", goal.GoalID);
            cmd.Parameters.AddWithValue("@UserID", goal.UserID);

            // handle optional DateEnded
            if (string.IsNullOrWhiteSpace(goal.DateEnded))
            {
                cmd.Parameters.AddWithValue("@DateEnded", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DateEnded", goal.DateEnded);
            }

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            cmd.ExecuteNonQuery();
        }
    }
    //this method updates an existing dail log's details in the 'users.tblDailyLog' table.
    public void UpdateDailyLog(DailyLog log)
    {
        //this is a slq query statement with paramters. This updates all daily log fields where the dailyLog matches the one of the selected daily log.
        string query = @"UPDATE users.tblDailyLog 
                     SET foodID = @FoodID, 
                         mealTime = @MealTime, 
                         dateLogged = @DateLogged 
                     WHERE logID = @LogID AND userID = @UserID";

        //add values to the SQL parameters from the passed DailyLog object. This also binds the actual input to the query to prevent SQL injection.
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FoodID", log.FoodID);
            cmd.Parameters.AddWithValue("@MealTime", log.MealTime);
            cmd.Parameters.AddWithValue("@DateLogged", log.DateLogged);
            cmd.Parameters.AddWithValue("@LogID", log.LogID);
            cmd.Parameters.AddWithValue("@UserID", log.UserID);

            //this is used to execute commands that do not return rows of data, instead stores the number of rows affected.
            cmd.ExecuteNonQuery();
        }
    }
    public void ViewAllUserDetails(string userType, int? TheID)
    {
        ConsoleView view = new ConsoleView();
        List<string[]> userList = new List<string[]>();

        //creates query based on user type, admin can view all while a user can only view theirs
        string query = (userType == "admin")
            ? "SELECT firstName, lastName, emailID, age, gender, userWeight, userHeight, signUpDate FROM users.tblUserDetails"
            : "SELECT firstName, lastName, emailID, age, gender, userWeight, userHeight, signUpDate FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            // If the viewer is not admin and a valid user ID is provided, link the parameter to the query.
            if (userType != "admin" && TheID.HasValue)
            {
                cmd.Parameters.AddWithValue("@UserID", TheID.Value);
            }

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //create a row to store one user's details.
                    string[] row = new string[8];
                    //format each field properly
                    row[0] = reader.GetString(0);
                    row[1] = reader.GetString(1);
                    row[2] = reader.GetString(2);
                    row[3] = reader.GetInt32(3).ToString();
                    row[4] = reader.GetString(4);
                    row[5] = Math.Round(reader.GetDecimal(5)).ToString();
                    row[6] = Math.Round(reader.GetDecimal(6)).ToString();
                    row[7] = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                    //add this row to list of users.
                    userList.Add(row);
                }
            }
        }
        //if no record are found display this message and exit method.
        if (userList.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        //make header and their max width for alignement
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
        //setting up a page view
        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        //pages controlled through loop
        while (true)
        {
            view.Clear("View User Details");

            //build headers and center the column headers accordingly to their wdith settings.
            string columnHeader = string.Join(" | ", headers.Select(h => cut(h.Item1, h.Item2)));
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            //print the border and the column headers (header in cyan for aesthetics)
            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            //calculateing here what sub-section (aka which ones) of the user list to display on THIS page.
            int totalPages = (int)Math.Ceiling((double)userList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, userList.Count);

            //print rows and format each field so it fits in the columns
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

            //the pages will only be needed if the user is admin because users will only see one record.
            if (userType == "admin")
            {
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(pad + "Press any other key to go back");
                Console.ResetColor();
                
                //page navigation (left and right arrows)
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

        //create the query to select all admin details from the table
        string query = "SELECT adminID, firstName, lastName, emailID, passwordkey FROM admins.tblAdminDetails";

        //use the query and read the result from the database
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                //create an AdminDetails object and fill with values from the database
                AdminDetails admin = new AdminDetails(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );
                //add this admin object to the list
                adminList.Add(admin);
            }
        }
        //setting up a page view
        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        //pages controlled through loop
        while (true)
        {
            view.Clear("View All Admins");
            Console.WriteLine();
            //build headers and center the column headers accordingly to their wdith settings.
            string columnHeader = string.Format("{0,-6}    {1,-15}    {2,-15}    {3,-25}", "ID", "First Name", "Last Name", "Email");
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            //print the border and the column headers (header in cyan for aesthetics)
            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad); // apply color to padding too
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            //calculateing here what sub-section (aka which ones) of the admin list to display on THIS page.
            int totalPages = (int)Math.Ceiling((double)adminList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, adminList.Count);

            if (adminList.Count == 0)
            {
                Console.WriteLine(pad + "No admin records found.");
            }
            else
            {
                //print rows and format each field so it fits in the columns
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

            //page navigation (left and right arrows)
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
            // If left/right pressed but not valid stay on same page
        }
    }
    public void ViewAllAllergies(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<(string Allergy, int AllergyID)> allergyList = new List<(string, int)>();

        //create the query to select allergy details from the table for the given user ID
        string query = "SELECT allergy, allergyID FROM users.tblAllergies WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);
            //use the query and read the result from the database
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //extract allergy and allergyID from result and add to the list
                    string allergy = reader.GetString(0);
                    int allergyID = reader.GetInt32(1);
                    allergyList.Add((allergy, allergyID));
                }
            }
        }
        //setting up a page view
        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        //pages controlled through loop
        while (true)
        {
            view.Clear("View All Alergies");

            //build and center the heading based on user ID
            string heading = $"Allergies for User ID: {userID}";
            int headingPad = Math.Max(0, (consoleWidth - heading.Length) / 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string(' ', headingPad) + heading);
            Console.ResetColor();
            Console.WriteLine();

            //build headers and center the column headers accordingly to their width settings.
            string columnHeader = string.Format("{0,-5}    {1,-30}", "ID", "Allergy");
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);

            //print the border and the column headers (header in cyan for aesthetics)
            Console.WriteLine(new string(' ', leftPad) + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string(' ', leftPad));
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(new string(' ', leftPad) + new string('-', tableWidth));

            //calculateing here what sub-section (aka which ones) of the allergy list to display on THIS page.
            int totalPages = (int)Math.Ceiling((double)allergyList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, allergyList.Count);

            if (allergyList.Count == 0)
            {
                Console.WriteLine(new string(' ', leftPad) + "No allergies recorded.");
            }
            else
            {
                //print rows and format each field so it fits in the columns
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

            //page navigation (left and right arrows)
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
    }
    public void ViewAllFoods()
    {
        ConsoleView view = new ConsoleView();
        List<Foods> foodList = new List<Foods>();

        //create the query to select food details from the table
        string query = "SELECT foodID, foodName, category, calories, carbohydrates, proteins, fats, servingSize FROM admins.tblFoods";

        //use the query and read the result from the database
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                //create a Foods object and fill with values from the database
                Foods food = new Foods(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetDecimal(3),
                    reader.GetDecimal(4),
                    reader.GetDecimal(5),
                    reader.GetDecimal(6),
                    reader.GetDecimal(7)
                );
                //add this food object to the list
                foodList.Add(food);
            }
        }
        //setting up a page view
        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        //make header and their max width for alignement
        var headers = new[]
        {
        ("ID", 4),
        ("Name", 20),
        ("Category", 15),
        ("Cal (g)", 5),
        ("Carbs (g)", 6),
        ("Prot (g)", 6),
        ("Fats (g)", 6),
        ("Size (g)", 6)
    };
        //pages controlled through loop
        while (true)
        {
            view.Clear("View All Foods");

            //build headers and center the column headers accordingly to their wdith settings.
            string columnHeader = string.Format("{0,-4}    {1,-20}    {2,-15}    {3,-5}    {4,-6}    {5,-6}    {6,-6}    {7,-6}",
                headers[0].Item1, headers[1].Item1, headers[2].Item1, headers[3].Item1,
                headers[4].Item1, headers[5].Item1, headers[6].Item1, headers[7].Item1);

            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            //print the border and the column headers (header in cyan for aesthetics)
            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            //calculateing here what sub-section (aka which ones) of the food list to display on THIS page.
            int totalPages = (int)Math.Ceiling((double)foodList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, foodList.Count);

            if (foodList.Count == 0)
            {
                Console.WriteLine(pad + "No foods available.");
            }
            else
            {
                //print rows and format each field so it fits in the columns
                for (int i = startIndex; i < endIndex; i++)
                {
                    var f = foodList[i];
                    string line = string.Format("{0,-4}    {1,-20}    {2,-15}    {3,-5}      {4,-6}       {5,-6}      {6,-6}      {7,-6}",
                        f.FoodID,
                        Truncate(f.FoodName, 20),
                        Truncate(f.Category, 15),
                        Math.Round(f.Calories),
                        Math.Round(f.Carbohydrates),
                        Math.Round(f.Proteins),
                        Math.Round(f.Fats),
                        Math.Round(f.ServingSize));

                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to go back");
            Console.ResetColor();

            //page navigation (left and right arrows)
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
    }
    public void ViewAllDietPlans()
    {
        ConsoleView view = new ConsoleView();
        List<DietPlans> planList = new List<DietPlans>();

        //create the query to select diet plan details from the table
        string query = "SELECT dietPlanID, dietPlan, caloriesTarget, proteinsTarget, carbohydratesTarget, fatsTarget FROM admins.tblDietPlans";

        //use the query and read the result from the database
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                //create a DietPlans object and fill with values from the database
                DietPlans plan = new DietPlans(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5)
                );
                //add this diet plan object to the list
                planList.Add(plan);
            }
        }
        //setting up a page view
        int pageSize = 20;
        int currentPage = 0;
        int consoleWidth = Console.WindowWidth;

        //make header and their max width for alignement
        var headers = new[]
        {
        ("ID", 4),
        ("Plan Name", 20),
        ("Cal Target (g)", 15),
        ("Protein Target (g)", 18),
        ("Carbs Target (g)", 17),
        ("Fats Target (g)", 16)
    };

        //pages controlled through loop
        while (true)
        {
            view.Clear("View All Diet Plans");

            //build headers and center the column headers accordingly to their wdith settings.
            string columnHeader = string.Format("{0,-4}    {1,-25}  {2,-7}    {3,-12}    {4,-12}    {5,-12}",
    headers[0].Item1,
    headers[1].Item1,
    headers[2].Item1,
    headers[3].Item1,
    headers[4].Item1,
    headers[5].Item1
);
            //print the border and the column headers (header in cyan for aesthetics)
            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            Console.WriteLine(columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            //calculateing here what sub-section (aka which ones) of the diet plan list to display on THIS page.
            int totalPages = (int)Math.Ceiling((double)planList.Count / pageSize);
            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, planList.Count);

            if (planList.Count == 0)
            {
                Console.WriteLine(pad + "No diet plans available.");
            }
            else
            {
                //print rows and format each field so it fits in the columns

                for (int i = startIndex; i < endIndex; i++)
                {
                    var p = planList[i];
                    string line = string.Format("{0,-4}    {1,-20}       {2,-15}   {3,-18}    {4,-17}   {5,-16}",
                        p.DietPlanID,
                        Truncate(p.DietPlan, 20),
                        p.CaloriesTarget,
                        p.ProteinsTarget,
                        p.CarbohydratesTarget,
                        p.FatsTarget
                    );

                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to go back");
            Console.ResetColor();

            //page navigation (left and right arrows)
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
    }
    public void ViewAllGoals(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<Goals> goalsList = new List<Goals>();

        //create the query to select goal details from the table for the given user ID
        string query = "SELECT goalID, userID, dietPlanID, goal, dateStarted, dateEnded FROM users.tblGoals WHERE userID = @UserID";

        //use the query and read the result from the database
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //create a Goals object and fill with values from the database
                    Goals g = new Goals(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDateTime(4).ToString("yyyy-MM-dd"),
                        reader.IsDBNull(5) ? null : reader.GetDateTime(5).ToString("yyyy-MM-dd")
                    );
                    //add this goal object to the list
                    goalsList.Add(g);
                }
            }
        }

        view.Clear("View Goals");
        //make header and their max width for alignement
        var headers = new[]
        {
        ("ID", 4),
        ("Goal", 25),
        ("PlanID", 7),
        ("Start Date", 12),
        ("End Date", 12)
    };
        //build headers and center the column headers accordingly to their width settings.
        string columnHeader = string.Format("{0,-4}    {1,-25}    {2,-7}    {3,-12}    {4,-12}",
            headers[0].Item1, headers[1].Item1, headers[2].Item1, headers[3].Item1, headers[4].Item1);

        int tableWidth = columnHeader.Length;
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
        string pad = new string(' ', leftPad);
        
        //print the border and the column headers (header in cyan for aesthetics)
        Console.WriteLine(pad + new string('-', tableWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(pad);
        Console.WriteLine(columnHeader);
        Console.ResetColor();
        Console.WriteLine(pad + new string('-', tableWidth));

        if (goalsList.Count == 0)
        {
            Console.WriteLine(pad + "No goals recorded.");
        }
        else
        {
            foreach (var g in goalsList)
            {
                //print rows and format each field so it fits in the columns
                string line = string.Format("{0,-4}    {1,-25}    {2,-7}    {3,-12}    {4,-12}",
                    g.GoalID,
                    Truncate(g.Goal, 25),
                    g.DietPlanID,
                    g.DateStarted,
                    g.DateEnded);
                Console.WriteLine(pad + line);
            }
        }

        Console.WriteLine(pad + new string('-', tableWidth));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    public void ViewAllDailyLogs(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<DailyLog> logList = new List<DailyLog>();

        //create the query to select daily log details from the table for the given user ID
        string query = "SELECT logID, userID, foodID, mealTime, dateLogged FROM users.tblDailyLog WHERE userID = @UserID";

        //use the query and read the result from the database
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //create a DailyLog object and fill with values from the database
                    DailyLog log = new DailyLog(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDateTime(4).ToString("yyyy-MM-dd")
                    );
                    //add this log object to the list
                    logList.Add(log);
                }
            }
        }

        view.Clear("View Daily Logs");

        //make header and their max width for alignement
        var headers = new[]
        {
        ("ID", 4),
        ("FoodID", 7),
        ("Meal", 10),
        ("Date", 12)
    };
        //build headers and center the column headers accordingly to their width settings
        string columnHeader = string.Format("{0,-4}     {1,-7}    {2,-10}    {3,-12}",
            headers[0].Item1, headers[1].Item1, headers[2].Item1, headers[3].Item1);

        int tableWidth = columnHeader.Length;
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
        string pad = new string(' ', leftPad);

        //print the border and the column headers (header in cyan for aesthetics)
        Console.WriteLine(pad + new string('-', tableWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(pad);
        Console.WriteLine(columnHeader);
        Console.ResetColor();
        Console.WriteLine(pad + new string('-', tableWidth));

        if (logList.Count == 0)
        {
            Console.WriteLine(pad + "No logs recorded.");
        }
        else
        {
            //print rows and format each field so it fits in the columns
            foreach (var log in logList)
            {
                string line = string.Format("{0,-4}    {1,-7}    {2,-10}    {3,-12}",
                    log.LogID,
                    log.FoodID,
                    Truncate(log.MealTime, 10),
                    log.DateLogged);
                Console.WriteLine(pad + line);
            }
        }

        Console.WriteLine(pad + new string('-', tableWidth));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    //truncates a string to a maximum length and add a single ellipsis if it overflows
    private string Truncate(string text, int maxLength)
    {
        if (text.Length <= maxLength)
            return text;
        //cuts off the sting and adds the ellipsis at the end.
        return text.Substring(0, maxLength - 1) + "…";
    }
    //this one is similar but used for a different reason. It is used to adjust a string to fit exactly into the widht of a column WITH padding.
    private string cut(string value, int width)
    {
        //short string = left pad with spaces
        if (value.Length < width)
        {
            string extraNeeded = new string(' ', width - value.Length);
            value = extraNeeded + value;
            return value;
        }
        // if string is longer than it should be, cut it and add ...
        if (value.Length > width)
        {
            return value.Substring(0, Math.Max(0, width - 3)) + "...";
        }
        //i fits perfect, right pad it to align with the formatting
        return value.PadRight(width);
    }
    public bool DeleteUserByID(int userID)
    {
        //create the SQL query to delete a user record by userID
        string query = "DELETE FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject userID into the query
            cmd.Parameters.AddWithValue("@UserID", userID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0; //true if row was deleted
        }
    }
    public bool DeleteAdminByID(int adminID)
    {
        //create the SQL query to delete an admin record by adminID
        string query = "DELETE FROM admins.tblAdminDetails WHERE adminID = @AdminID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject adminID into the query
            cmd.Parameters.AddWithValue("@AdminID", adminID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0; //true if row was deleted
        }
    }
    public bool DeleteAllergyByID(int allergyID, int userID)
    {
        ////create the SQL query to delete an allergy record using both allergyID and userID
        string query = @"DELETE FROM users.tblAllergies 
                     WHERE allergyID = @AllergyID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameters to safely inject allergyID and userID into the query
            cmd.Parameters.AddWithValue("@AllergyID", allergyID);
            cmd.Parameters.AddWithValue("@UserID", userID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;//true if row was deleted
        }
    }
    public bool DeleteFoodByID(int foodID)
    {
        //create the SQL query to delete a food record by foodID
        string query = "DELETE FROM admins.tblFoods WHERE foodID = @FoodID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject foodID into the query
            cmd.Parameters.AddWithValue("@FoodID", foodID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0; //true if row was deleted
        }
    }
    public bool DeleteDietPlanByID(int dietPlanID)
    {
        //create the SQL query to delete a diet plan record by dietPlanID
        string query = "DELETE FROM admins.tblDietPlans WHERE dietPlanID = @DietPlanID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject dietPlanID into the query
            cmd.Parameters.AddWithValue("@DietPlanID", dietPlanID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;//true if row was deleted
        }
    }
    public bool DeleteGoalByID(int goalID, int userID)
    {
        //create the SQL query to delete a goal using both goalID and userID
        string query = @"DELETE FROM users.tblGoals 
                     WHERE goalID = @GoalID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameters to safely inject goalID and userID into the query
            cmd.Parameters.AddWithValue("@GoalID", goalID);
            cmd.Parameters.AddWithValue("@UserID", userID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0; //true if the row was deleted
        }
    }
    public bool DeleteDailyLogByID(int logID, int userID)
    {
        //create the SQL query to delete a daily log using both logID and userID
        string query = @"DELETE FROM users.tblDailyLog 
                     WHERE logID = @LogID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameters to safely inject logID and userID into the query
            cmd.Parameters.AddWithValue("@LogID", logID);
            cmd.Parameters.AddWithValue("@UserID", userID);
            //execute the delete command and store how many rows were affected
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0; //true if row was deleted
        }
    }
    public UserDetails GetUserByID(int id)
    {
        //create the SQL query to retrieve a user record by userID
        string query = "SELECT * FROM users.tblUserDetails WHERE userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject userID into the query
            cmd.Parameters.AddWithValue("@UserID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return a UserDetails object with data from the row
                    return new UserDetails(
                        id,
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetInt32(5),
                        reader.GetString(6),
                        Convert.ToDouble(reader.GetDecimal(7)),
                        Convert.ToDouble(reader.GetDecimal(8)),
                        reader.GetDateTime(9).ToString("yyyy-MM-dd")
                    );
                }
            }
        }
        //return null if no user record was found
        return null;
    }
    public AdminDetails GetAdminByID(int id)
    {
        //create the SQL query to retrieve an admin record by adminID
        string query = "SELECT * FROM admins.tblAdminDetails WHERE adminID = @AdminID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject adminID into the query
            cmd.Parameters.AddWithValue("@AdminID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return an AdminDetails object with data from the row
                    return new AdminDetails(
                        id,
                        reader.GetString(1),
                        reader.GetString(2), 
                        reader.GetString(3),
                        reader.GetString(4) 
                    );
                }
            }
        }
        //return null if no admin record was found
        return null;
    }
    public Foods GetFoodByID(int id)
    {
        //create the SQL query to retrieve a food record by foodID
        string query = "SELECT * FROM admins.tblFoods WHERE foodID = @FoodID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject foodID into the query
            cmd.Parameters.AddWithValue("@FoodID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return a Foods object with data from the row
                    return new Foods(
                        id,
                        reader.GetString(1),
                        reader.GetString(2), 
                        reader.GetDecimal(3),
                        reader.GetDecimal(4), 
                        reader.GetDecimal(5), 
                        reader.GetDecimal(6),
                        reader.GetDecimal(7) 
                    );
                }
            }
        }
        //return null if no food record was found
        return null;
    }
    public DietPlans GetDietPlanByID(int id)
    {
        //create the SQL query to retrieve a diet plan record by dietPlanID
        string query = "SELECT * FROM admins.tblDietPlans WHERE dietPlanID = @DietPlanID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameter to safely inject dietPlanID into the query
            cmd.Parameters.AddWithValue("@DietPlanID", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return a DietPlans object with data from the row
                    return new DietPlans(
                        id,
                        reader.GetString(1), 
                        reader.GetInt32(2),   
                        reader.GetInt32(3),        
                        reader.GetInt32(4),  
                        reader.GetInt32(5)
                    );
                }
            }
        }
        //return null if no diet plan record was found
        return null;
    }
    public Goals GetGoalByID(int goalID, int userID)
    {
        //create the SQL query to retrieve a goal record by goalID and userID
        string query = "SELECT * FROM users.tblGoals WHERE goalID = @GoalID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameters to safely inject goalID and userID into the query
            cmd.Parameters.AddWithValue("@GoalID", goalID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return a Goals object with data from the row
                    return new Goals(
                        reader.GetInt32(0), 
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDateTime(4).ToString("yyyy-MM-dd"),
                        reader.IsDBNull(5) ? "" : reader.GetDateTime(5).ToString("yyyy-MM-dd") 
                    );
                }
            }
        }
        //return null if no goal record was found
        return null;
    }
    public DailyLog GetDailyLogByID(int logID, int userID)
    {
        //create the SQL query to retrieve a daily log record by logID and userID
        string query = "SELECT * FROM users.tblDailyLog WHERE logID = @LogID AND userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            //add parameters to safely inject logID and userID into the query
            cmd.Parameters.AddWithValue("@LogID", logID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //check if any row was returned
                if (reader.Read())
                {
                    //construct and return a DailyLog object with data from the row
                    return new DailyLog(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDateTime(4).ToString("yyyy-MM-dd")
                    );
                }
            }
        }
        //return null if no daily log record was found
        return null;
    }
    public int GetUserID(int userID, string passwordkey)
    {
        //create the SQL query to check if a user exists with the given userID and passwordkey
        using (SqlCommand cmd = new SqlCommand("SELECT userID FROM users.tblUserDetails WHERE userID = @userID AND passwordkey = @passwordkey", conn))
        {
            //add parameters to safely inject userID and passwordkey into the query
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@passwordkey", passwordkey);

            //execute the query and check if a result was returned
            if (cmd.ExecuteScalar() != null)
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                return 0;
            }
        }
    }
    public int GetAdminID(int adminID, string passwordkey)
    {
        //create the SQL query to check if an admin exists with the given adminID and passwordkey
        using (SqlCommand cmd = new SqlCommand("SELECT adminID FROM admins.tblAdminDetails WHERE adminID = @adminID AND passwordkey = @passwordkey", conn))
        {
            //add parameters to safely inject adminID and passwordkey into the query
            cmd.Parameters.AddWithValue("@adminID", adminID);
            cmd.Parameters.AddWithValue("@passwordkey", passwordkey);

            //executeing the queryy and taking actions according to the result
            if (cmd.ExecuteScalar() != null)
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                return 0;
            }
        }
    }
    
    //These following queries as all pretty simples using the same leements as above with naviagtion using arrows, using sql queries which are used to extract data from the DB and then displaying it with nice formatting.
    public void CalculateBMI()
    {
        ConsoleView view = new ConsoleView();
        List<(string FullName, decimal BMI, string Status)> bmiList = new List<(string, decimal, string)>();

        string query = @"
        SELECT 
            (D.firstName + ' ' + D.lastName) AS [User], 
            CONVERT(DECIMAL(10,2), D.userWeight/((D.userHeight/100) * (D.userHeight/100))) AS [BMI],
            CASE
                WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) < 18.5 THEN 'Underweight'
                WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) BETWEEN 18.5 AND 24.9 THEN 'Normal weight'
                WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) BETWEEN 25.0 AND 29.9 THEN 'Overweight'
                ELSE 'Obese'
            END AS [Status]
        FROM users.tblUserDetails D";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string name = reader.GetString(0);
                decimal bmi = reader.GetDecimal(1);
                string status = reader.GetString(2);
                bmiList.Add((name, bmi, status));
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)bmiList.Count / pageSize);
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("User BMI Overview");

            string header = string.Format("{0,-25}    {1,-6}    {2,-15}", "User", "BMI", "Status");
            int tableWidth = header.Length;
            string pad = new string(' ', Math.Max(0, (consoleWidth - tableWidth) / 2));

            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(pad + header);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, bmiList.Count);

            if (bmiList.Count == 0)
            {
                Console.WriteLine(pad + "No data found.");
            }
            else
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    var entry = bmiList[i];
                    string line = string.Format("{0,-25}    {1,-6}    {2,-15}",
                        Truncate(entry.FullName, 25),
                        entry.BMI,
                        Truncate(entry.Status, 15));
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
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
            else
            {
                break;
            }
        }
    }
    public void Top25Users()
    {
        ConsoleView view = new ConsoleView();
        List<(string Name, decimal AvgCalories, int TotalLogs)> resultList = new List<(string, decimal, int)>();

        string query = @"
        SELECT TOP 25
            D.firstName + ' ' + D.lastName AS [Name],
            CONVERT(DECIMAL(10,2), ROUND(AVG(F.calories), 2)) AS [Avg Calories (g)],
            COUNT(*) AS [Total Logs]
        FROM users.tblUserDetails D, users.tblDailyLog L, admins.tblFoods F
        WHERE D.userID = L.userID AND F.foodID = L.foodID
        GROUP BY D.firstName + ' ' + D.lastName
        ORDER BY [Total Logs] DESC;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string name = reader.GetString(0);
                decimal avgCalories = reader.GetDecimal(1);
                int totalLogs = reader.GetInt32(2);
                resultList.Add((name, avgCalories, totalLogs));
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)resultList.Count / pageSize);
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("Top 25 Users by Logs");

            string columnHeader = string.Format("{0,-25}    {1,-18}    {2,-12}",
                "Name", "Avg Calories (g)", "Total Logs");
            int tableWidth = columnHeader.Length;
            string pad = new string(' ', Math.Max(0, (consoleWidth - tableWidth) / 2));

            Console.WriteLine(pad + new string('-', tableWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(pad + columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            int startIndex = currentPage * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, resultList.Count);

            if (resultList.Count == 0)
            {
                Console.WriteLine(pad + "No data available.");
            }
            else
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    var item = resultList[i];
                    string line = string.Format("{0,-25}    {1,-18}    {2,-12}",
                        Truncate(item.Name, 25), item.AvgCalories, item.TotalLogs);
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
            else
            {
                break;
            }
        }
    }
    public void UserDemographic()
    {
        ConsoleView view = new ConsoleView();
        List<(string AgeGroup, int Male, int Female, int NotSpecified, int Total)> resultList = new List<(string, int, int, int, int)>();

        string query = @"
        SELECT
        CASE
        WHEN D.age < 18 THEN 'Under 18'
        WHEN D.age BETWEEN 18 AND 35 THEN '18–35'
        WHEN D.age BETWEEN 36 AND 60 THEN '36–60'
        ELSE 'Over 60'
        END AS [ ],
        COUNT(CASE WHEN D.gender = 'MALE' THEN 1 END) AS [Male],
        COUNT(CASE WHEN D.gender = 'FEMALE' THEN 1 END) AS [Female],
        COUNT(CASE WHEN D.gender = 'NULL' THEN 1 END) AS [Not Specified],
        COUNT(*) AS [Total]
    FROM
        users.tblUserDetails D
    GROUP BY
        CASE
        WHEN age < 18 THEN 'Under 18'
        WHEN age BETWEEN 18 AND 35 THEN '18–35'
        WHEN age BETWEEN 36 AND 60 THEN '36–60'
        ELSE 'Over 60'
        END,
        CASE
        WHEN D.age < 18 THEN 1
        WHEN D.age BETWEEN 18 AND 35 THEN 2
        WHEN D.age BETWEEN 36 AND 60 THEN 3
        ELSE 4
        END
    ORDER BY
        CASE
        WHEN age < 18 THEN 1
        WHEN age BETWEEN 18 AND 35 THEN 2
        WHEN age BETWEEN 36 AND 60 THEN 3
        ELSE 4
        END;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string ageGroup = reader.GetString(0);
                int male = reader.GetInt32(1);
                int female = reader.GetInt32(2);
                int notSpecified = reader.GetInt32(3);
                int total = reader.GetInt32(4);
                resultList.Add((ageGroup, male, female, notSpecified, total));
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling(resultList.Count / (double)pageSize);
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("Age and Gender Breakdown");

            string columnHeader = string.Format("{0,-15}    {1,-6}    {2,-6}    {3,-14}    {4,-6}",
                "Age Group", "Male", "Female", "Not Specified", "Total");

            int tableWidth = columnHeader.Length;
            int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
            string pad = new string(' ', leftPad);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(pad + columnHeader);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', tableWidth));

            int start = currentPage * pageSize;
            int end = Math.Min(start + pageSize, resultList.Count);

            if (resultList.Count == 0)
            {
                Console.WriteLine(pad + "No records found.");
            }
            else
            {
                for (int i = start; i < end; i++)
                {
                    var record = resultList[i];
                    string line = string.Format("{0,-15}    {1,-6}    {2,-6}    {3,-14}    {4,-6}",
                        record.AgeGroup, record.Male, record.Female, record.NotSpecified, record.Total);
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {totalPages}");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
            Console.ResetColor();

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && currentPage > 0)
            {
                currentPage--;
            }
            else if (key.Key == ConsoleKey.RightArrow && currentPage < totalPages - 1)
            {
                currentPage++;
            }
            else
            {
                break;
            }
        }
    }
    public void DetailedUserInfo(int TheID, string userType)
    {
        ConsoleView view = new ConsoleView();
        List<string[]> rows = new List<string[]>();

        string query = @"
        SELECT 
            U.userID AS [ID], 
            U.firstName AS [First Name], 
            U.lastName AS [Last Name], 
            U.emailID AS [Email], 
            U.passwordkey AS [Password], 
            U.age AS [Age], 
            U.gender AS [Gender], 
            CAST(ROUND(U.userWeight, 0) AS INT) AS [Weight (kg)],
            CAST(ROUND(U.userHeight, 0) AS INT) AS [Height (cm)],
            CONVERT(varchar(10), U.signUpDate, 111) AS [Date Joined (yyyy/mm/dd)]
        FROM users.tblUserDetails U
        WHERE @userID = 0 OR U.userID = @userID;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (userType.ToLower() == "admin")
            {
                TheID = 0;
            }
            cmd.Parameters.AddWithValue("@UserID", TheID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string[] row = new string[10];
                    for (int i = 0; i < row.Length; i++)
                    {
                        row[i] = reader[i].ToString();
                    }
                    rows.Add(row);
                }
            }
        }

        string[] headers = { "ID", "First", "Last", "Email", "Password", "Age", "Gender", "Weight", "Height", "Joined" };
        int[] widths = { 4, 8, 8, 20, 10, 4, 5, 5, 5, 10 };

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)rows.Count / pageSize);
        int consoleWidth = Console.WindowWidth;

        while (true)
        {
            view.Clear("User Details");

            int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
            int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
            string pad = new string(' ', leftPad);

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(pad + headers[i].PadRight(widths[i]) + "   ");
                pad = ""; // prevent double padding
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string(' ', leftPad) + new string('-', totalWidth));


            if (rows.Count == 0)
            {
                Console.WriteLine(new string(' ', leftPad) + "No user records found.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, rows.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    string line = "";
                    for (int j = 0; j < headers.Length; j++)
                    {
                        string cell = Truncate(rows[i][j], widths[j]);
                        line += cell.PadRight(widths[j]) + "   ";
                    }
                    Console.WriteLine(new string(' ', leftPad) + line);
                }


                Console.WriteLine(new string(' ', leftPad) + new string('-', totalWidth));
                if (userType == "admin")
                {
                    Console.WriteLine(new string(' ', leftPad) + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
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
            else
            {
                break;
            }
        }
    }
    public void AverageGoalDurations()
    {
        ConsoleView view = new ConsoleView();
        List<(int ID, string Goal, int AvgDuration)> resultList = new List<(int, string, int)>();

        string query = @"
    SELECT
        G.goalID AS [ID],
        G.goal AS [Goal],
        AVG(DATEDIFF(DAY, G.dateStarted, G.dateEnded)) AS [Avg Duration (days)]
    FROM
        users.tblGoals G
    WHERE
        G.dateStarted IS NOT NULL 
        AND G.dateEnded IS NOT NULL AND G.dateEnded <> ''
    GROUP BY
        G.goal,
        G.goalID
    ORDER BY
        [Avg Duration (days)];";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                int goalID = reader.GetInt32(0);
                string goal = reader.GetString(1);
                int avgDays = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);

                resultList.Add((goalID, goal, avgDays));
            }
        }

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)resultList.Count / pageSize);

        string[] headers = { "ID", "Goal", "Avg Duration (days)" };
        int[] widths = { 5, 30, 20 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        while (true)
        {
            view.Clear("Goal Duration Summary");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;

            string headerLine = string.Format("{0,-5}   {1,-30}   {2,-20}",
                headers[0], headers[1], headers[2]);
            Console.WriteLine(pad + headerLine);
            Console.ResetColor();
            Console.WriteLine(pad + new string('-', totalWidth));

            if (resultList.Count == 0)
            {
                Console.WriteLine(pad + "No completed goals to calculate duration.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, resultList.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var row = resultList[i];
                    string line = string.Format("{0,-5}   {1,-30}   {2,-20}",
                        row.ID,
                        Truncate(row.Goal, 30),
                        row.AvgDuration.ToString());
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return");
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
            else
            {
                break;
            }
        }
    }
    public void AverageCaloriesPerMealTime()
    {
        ConsoleView view = new ConsoleView();
        List<(string Meal, decimal AvgCalories)> resultList = new List<(string, decimal)>();

        string query = @"
    SELECT
      L.mealTime AS [Meal],
      CONVERT(DECIMAL(10,2), AVG(F.calories)) AS [Avg Calories (g)]
    FROM
      users.tblDailyLog L, admins.tblFoods F
    WHERE
      L.foodID = F.foodID
    GROUP BY
      L.mealTime
    ORDER BY
      L.mealTime;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string meal = reader.GetString(0);
                decimal avgCalories = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);
                resultList.Add((meal, avgCalories));
            }
        }

        view.Clear("Average Calories Per Meal");

        string[] headers = { "Meal", "Avg Calories (g)" };
        int[] widths = { 20, 20 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int leftPad = Math.Max(0, (Console.WindowWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        Console.WriteLine(pad + new string('-', totalWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        string headerLine = string.Format("{0,-20}   {1,-20}", headers[0], headers[1]);
        Console.WriteLine(pad + headerLine);
        Console.ResetColor();
        Console.WriteLine(pad + new string('-', totalWidth));

        if (resultList.Count == 0)
        {
            Console.WriteLine(pad + "No meal logs found.");
        }
        else
        {
            foreach (var row in resultList)
            {
                string line = string.Format("{0,-20}   {1,-20}", Truncate(row.Meal, 20), row.AvgCalories);
                Console.WriteLine(pad + line);
            }

            Console.WriteLine(pad + new string('-', totalWidth));
        }

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    public void MealTimesLogged()
    {
        ConsoleView view = new ConsoleView();
        List<string[]> results = new List<string[]>();

        string query = @"
    -- Row 1: Total logs per meal
    SELECT
      'Total Logs' AS [ ],
      CAST(COUNT(CASE WHEN L.mealTime = 'BREAKFAST' THEN 1 END) AS VARCHAR) AS [Breakfast],
      CAST(COUNT(CASE WHEN L.mealTime = 'LUNCH' THEN 1 END) AS VARCHAR) AS [Lunch],
      CAST(COUNT(CASE WHEN L.mealTime = 'DINNER' THEN 1 END) AS VARCHAR) AS [Dinner],
      CAST(COUNT(*) AS VARCHAR) AS [Total]
    FROM admins.tblFoods F
    JOIN users.tblDailyLog L ON F.foodID = L.foodID

    UNION ALL

    SELECT
      'Most Logged Food' AS [ ],
      MAX(CASE WHEN mealTime = 'BREAKFAST' THEN foodName END) AS [Breakfast],
      MAX(CASE WHEN mealTime = 'LUNCH' THEN foodName END) AS [Lunch],
      MAX(CASE WHEN mealTime = 'DINNER' THEN foodName END) AS [Dinner],
      ' ' AS [Total]
    FROM (
      SELECT
        L.mealTime,
        F.foodName,
        COUNT(*) AS occurrences,
        ROW_NUMBER() OVER (PARTITION BY L.mealTime ORDER BY COUNT(*) DESC) AS rowNo
      FROM users.tblDailyLog L
      JOIN admins.tblFoods F ON F.foodID = L.foodID
      GROUP BY L.mealTime, F.foodName
    ) AS RankedFoods
    WHERE rowNo = 1;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string[] row = new string[5];
                for (int i = 0; i < 5; i++)
                {
                    row[i] = reader[i].ToString();
                }
                results.Add(row);
            }
        }

        view.Clear("Meal Log Summary");

        string[] headers = { "", "Breakfast", "Lunch", "Dinner", "Total" };
        int[] widths = { 20, 20, 20, 20, 10 };
        int totalWidth = widths.Sum() + (headers.Length - 1) * 3;
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        Console.WriteLine(pad + new string('-', totalWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        string headerLine = string.Format("{0,-20}   {1,-20}   {2,-20}   {3,-20}   {4,-10}",
            headers[0], headers[1], headers[2], headers[3], headers[4]);
        Console.WriteLine(pad + headerLine);
        Console.ResetColor();
        Console.WriteLine(pad + new string('-', totalWidth));

        foreach (var row in results)
        {
            string line = string.Format("{0,-20}   {1,-20}   {2,-20}   {3,-20}   {4,-10}",
                Truncate(row[0], 20),
                Truncate(row[1], 20),
                Truncate(row[2], 20),
                Truncate(row[3], 20),
                Truncate(row[4], 10));
            Console.WriteLine(pad + line);
        }

        Console.WriteLine(pad + new string('-', totalWidth));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    public void UserAverageCalories()
    {
        ConsoleView view = new ConsoleView();
        List<(int ID, string Name, int TotalLogs, int TotalCalories, double AvgCalories)> resultList = new List<(int, string, int, int, double)>();

        string query = @"
    SELECT
      D.userID AS [ID],
      (D.firstName + ' ' + D.lastName) AS [Name],
      COUNT(*) AS [Total Logs],
      SUM(F.calories) AS [Total Calories (g)],
      CONVERT(DECIMAL(10,2), AVG(F.calories)) AS [Avg Calories Per log (g)]
    FROM
      users.tblDailyLog L, admins.tblFoods F, users.tblUserDetails D
    WHERE
      L.foodID = F.foodID
      and D.userID = L.userID
    GROUP BY
      D.userID, 
      D.firstName + ' ' + D.lastName
    ORDER BY
      [Avg Calories Per log (g)];";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int totalLogs = reader.GetInt32(2);
                int totalCalories = Convert.ToInt32(reader.GetDecimal(3));
                int avgCalories = Convert.ToInt32(reader.GetDecimal(4));

                resultList.Add((id, name, totalLogs, totalCalories, avgCalories));
            }
        }

        string[] headers = { "ID", "Name", "Logs", "Total Cal", "Avg Cal/Log" };
        int[] widths = { 5, 20, 10, 12, 15 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int leftPad = Math.Max(0, (Console.WindowWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)resultList.Count / pageSize);

        while (true)
        {
            view.Clear("Calories Per User");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(widths[i]) + "   ");
            }
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine(pad + new string('-', totalWidth));

            if (resultList.Count == 0)
            {
                Console.WriteLine(pad + "No user logs found.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, resultList.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var row = resultList[i];
                    string line = string.Format("{0,-5}   {1,-20}   {2,-10}   {3,-12}   {4,-15}",
                        row.ID,
                        Truncate(row.Name, 20),
                        row.TotalLogs,
                        row.TotalCalories,
                        Math.Round(row.AvgCalories, 2));
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
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
            else
            {
                break;
            }
        }
    }
    public void ViewFoodsByCategory()
    {
        ConsoleView view = new ConsoleView();
        var foodMap = new Dictionary<string, List<Foods>>();

        string query = @"
        SELECT 
            foodID, foodName, category, calories, carbohydrates, proteins, fats, servingSize 
        FROM admins.tblFoods";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Foods food = new Foods(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetDecimal(3),
                    reader.GetDecimal(4),
                    reader.GetDecimal(5),
                    reader.GetDecimal(6),
                    reader.GetDecimal(7)
                );

                string category = food.Category.ToUpper();

                if (!foodMap.ContainsKey(category))
                {
                    foodMap[category] = new List<Foods>();
                }
                foodMap[category].Add(food);
            }
        }

        var categoryList = foodMap.Keys.OrderBy(c => c).ToList();
        int currentCategoryIndex = 0;

        while (true)
        {
            string category = categoryList[currentCategoryIndex];
            List<Foods> foodList = foodMap[category];

            int pageSize = 20;
            int currentPage = 0;
            int totalPages = (int)Math.Ceiling((double)foodList.Count / pageSize);
            int consoleWidth = Console.WindowWidth;

            string[] headers = { "ID", "Name", "Category", "Cal", "Carbs", "Prot", "Fats", "Size" };
            int[] widths = { 4, 20, 15, 5, 6, 6, 6, 6 };

            while (true)
            {
                view.Clear($"Foods - Category: {category}");

                string columnHeader = string.Format("{0,-4}    {1,-20}    {2,-15}    {3,-5}    {4,-6}    {5,-6}    {6,-6}    {7,-6}",
                    headers[0], headers[1], headers[2], headers[3], headers[4], headers[5], headers[6], headers[7]);

                int tableWidth = columnHeader.Length;
                int leftPad = Math.Max(0, (consoleWidth - tableWidth) / 2);
                string pad = new string(' ', leftPad);

                Console.WriteLine(pad + new string('-', tableWidth));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(pad);
                Console.WriteLine(columnHeader);
                Console.ResetColor();
                Console.WriteLine(pad + new string('-', tableWidth));

                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, foodList.Count);

                if (foodList.Count == 0)
                {
                    Console.WriteLine(pad + "No foods in this category.");
                }
                else
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        var f = foodList[i];
                        string line = string.Format("{0,-4}    {1,-20}    {2,-15}    {3,-5}    {4,-6}    {5,-6}    {6,-6}    {7,-6}",
                            f.FoodID,
                            Truncate(f.FoodName, 20),
                            Truncate(f.Category, 15),
                            Math.Round(f.Calories),
                            Math.Round(f.Carbohydrates),
                            Math.Round(f.Proteins),
                            Math.Round(f.Fats),
                            Math.Round(f.ServingSize)
                        );
                        Console.WriteLine(pad + line);
                    }

                    Console.WriteLine(pad + new string('-', tableWidth));
                    Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)} - Category {currentCategoryIndex + 1} of {categoryList.Count}");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(pad + "Use ← → for pages, ↑ for next category, ↓ for previous. Press any other key to exit.");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (currentPage > 0)
                    {
                        currentPage--;
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (currentPage < totalPages - 1)
                    {
                        currentPage++;
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (currentCategoryIndex < categoryList.Count - 1)
                    {
                        currentCategoryIndex++;
                    }
                    break;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (currentCategoryIndex > 0)
                    {
                        currentCategoryIndex--;
                    }
                    break; 
                }
                else
                {
                    return;
                }
            }
        }
    }
    public void ViewUserGoals(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<(int ID, string Goal, string Started, string Ended)> goalsList = new List<(int, string, string, string)>();

        string query = @"
    SELECT G.goalID, G.goal, G.dateStarted, G.dateEnded
    FROM users.tblGoals G
    WHERE G.userID = @UserID";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string goal = reader.GetString(1);

                    DateTime startedDate = reader.GetDateTime(2);
                    string started = startedDate.ToString("yyyy-MM-dd");

                    string ended;
                    if (reader.IsDBNull(3))
                    {
                        ended = "N/A";
                    }
                    else
                    {
                        DateTime endDate = reader.GetDateTime(3);

                        if (endDate == new DateTime(1900, 1, 1) || endDate == DateTime.MinValue)
                        {
                            ended = "N/A";
                        }
                        else
                        {
                            ended = endDate.ToString("yyyy-MM-dd");
                        }
                    }
                    goalsList.Add((id, goal, started, ended));
                }
            }
        }

        string[] headers = { "ID", "Goal", "Started On", "Ended On" };
        int[] widths = { 5, 30, 15, 15 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)goalsList.Count / pageSize);

        while (true)
        {
            view.Clear("User Goals");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;

            string headerLine = string.Format("{0,-5}   {1,-30}   {2,-15}   {3,-15}",
                headers[0], headers[1], headers[2], headers[3]);
            Console.WriteLine(pad + headerLine);

            Console.ResetColor();
            Console.WriteLine(pad + new string('-', totalWidth));

            if (goalsList.Count == 0)
            {
                Console.WriteLine(pad + "No goals found for this user.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, goalsList.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var g = goalsList[i];
                    string line = string.Format("{0,-5}   {1,-30}   {2,-15}   {3,-15}",
                        g.ID,
                        Truncate(g.Goal, 30),
                        g.Started,
                        g.Ended);
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + "Page " + (currentPage + 1) + " of " + Math.Max(totalPages, 1) + ". Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (currentPage > 0)
                {
                    currentPage--;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (currentPage < totalPages - 1)
                {
                    currentPage++;
                }
            }
            else
            {
                break;
            }
        }
    }
    public void FoodByCalories()
    {
        ConsoleView view = new ConsoleView();
        List<(string Name, decimal Calories)> foodList = new List<(string, decimal)>();

        string query = @"SELECT foodName, calories FROM admins.tblFoods ORDER BY calories";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string name = reader.GetString(0);
                decimal calories = reader.GetDecimal(1);
                foodList.Add((name, calories));
            }
        }

        string[] headers = { "Name", "Calories (g)" };
        int[] widths = { 30, 15 };
        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)foodList.Count / pageSize);
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int leftPad = Math.Max(0, (Console.WindowWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        while (true)
        {
            view.Clear("Foods Sorted by Calories");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(widths[i]) + "   ");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(pad + new string('-', totalWidth));

            if (foodList.Count == 0)
            {
                Console.WriteLine(pad + "No foods found.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, foodList.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var f = foodList[i];
                    string line = string.Format("{0,-30}   {1,-15}", Truncate(f.Name, 30), f.Calories);
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (currentPage > 0)
                {
                    currentPage--;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (currentPage < totalPages - 1)
                {
                    currentPage++;
                }
            }
            else
            {
                break;
            }
        }
    }
    public void Top10Foods()
    {
        ConsoleView view = new ConsoleView();
        List<(string Name, decimal Calories, int TimesLogged)> foodList = new List<(string, decimal, int)>();

        string query = @"
    SELECT TOP 10
        F.foodName AS [Name],
        F.calories AS [Calories (g)],
        COUNT(*) AS [No. Times Logged]
    FROM
        admins.tblFoods F, users.tblDailyLog L
    WHERE
        F.foodID = L.foodID
    GROUP BY
        F.foodName,
        F.calories
    ORDER BY
        [No. Times Logged] DESC;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string name = reader.GetString(0);
                decimal calories = reader.GetDecimal(1);
                int timesLogged = reader.GetInt32(2);
                foodList.Add((name, calories, timesLogged));
            }
        }

        view.Clear("Top 10 Most Logged Foods");

        string[] headers = { "Name", "Calories (g)", "No. Times Logged" };
        int[] widths = { 30, 15, 20 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int leftPad = Math.Max(0, (Console.WindowWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        Console.WriteLine(pad + new string('-', totalWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(pad);
        for (int i = 0; i < headers.Length; i++)
        {
            Console.Write(headers[i].PadRight(widths[i]) + "   ");
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(pad + new string('-', totalWidth));

        if (foodList.Count == 0)
        {
            Console.WriteLine(pad + "No records found.");
        }
        else
        {
            foreach (var row in foodList)
            {
                string line = string.Format("{0,-30}   {1,-15}   {2,-20}",
                    Truncate(row.Name, 30),
                    row.Calories,
                    row.TimesLogged);
                Console.WriteLine(pad + line);
            }
            Console.WriteLine(pad + new string('-', totalWidth));
        }

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    public void DietPlanTargets()
    {
        ConsoleView view = new ConsoleView();
        List<string[]> results = new List<string[]>();

        string query = @"
    SELECT
      P.dietPlanID AS [ID],
      P.dietPlan AS [Diet Plan Name],
      CONVERT(VARCHAR(10), CAST(AVG(F.calories) * 100.0 / P.caloriesTarget AS DECIMAL(5,2))) + '%' AS [Calorie Target],
      CONVERT(VARCHAR(10), CAST(AVG(F.proteins) * 100.0 / P.proteinsTarget AS DECIMAL(5,2))) + '%' AS [Protein Target],
      CONVERT(VARCHAR(10), CAST(AVG(F.carbohydrates) * 100.0 / P.carbohydratesTarget AS DECIMAL(5,2))) + '%' AS [Carbs Target],
      CONVERT(VARCHAR(10), CAST(AVG(F.fats) * 100.0 / P.fatsTarget AS DECIMAL(5,2))) + '%' AS [Fat Target],
      CONVERT(VARCHAR(10), CAST((
        (AVG(F.calories) * 100.0 / P.caloriesTarget) +
        (AVG(F.proteins) * 100.0 / P.proteinsTarget) +
        (AVG(F.carbohydrates) * 100.0 / P.carbohydratesTarget) +
        (AVG(F.fats) * 100.0 / P.fatsTarget)
      ) / 4 AS DECIMAL(5,2))) + '%' AS [Overall Target Met]
    FROM
      users.tblUserDetails D, users.tblDailyLog L, admins.tblFoods F, admins.tblDietPlans P
    WHERE
      D.userID = L.userID AND F.foodID = L.foodID
    GROUP BY
      P.dietPlanID, P.dietPlan, P.caloriesTarget, P.carbohydratesTarget, P.proteinsTarget, P.fatsTarget
    ORDER BY [Overall Target Met] DESC;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string[] row = new string[7];
                for (int i = 0; i < row.Length; i++)
                {
                    row[i] = reader[i].ToString();
                }
                results.Add(row);
            }
        }

        string[] headers = { "ID", "Name", "Cal %", "Protein %", "Carbs %", "Fat %", "Overall %" };
        int[] widths = { 4, 20, 10, 12, 10, 10, 12 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)results.Count / pageSize);

        while (true)
        {
            view.Clear("Diet Plan Target Summary");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);

            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(widths[i]) + "   ");
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(pad + new string('-', totalWidth));

            if (results.Count == 0)
            {
                Console.WriteLine(pad + "No records found.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, results.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    string line = "";
                    for (int j = 0; j < headers.Length; j++)
                    {
                        line += Truncate(results[i][j], widths[j]).PadRight(widths[j]) + "   ";
                    }
                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && currentPage > 0)
            {
                currentPage--;
            }
            else if (key.Key == ConsoleKey.RightArrow && currentPage < totalPages - 1)
            {
                currentPage++;
            }
            else if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
            {
                break;
            }
        }
    }
    public void FoodsRelatedtoAllergies(int userID)
    {
        ConsoleView view = new ConsoleView();
        List<(string Allergy, string Food)> resultList = new List<(string, string)>();

        string query = @"
    SELECT A.allergy, F.foodName
    FROM users.tblAllergies A
    LEFT JOIN admins.tblFoods F ON F.foodName LIKE CONCAT('%', A.allergy, '%')
    WHERE A.userID = @userID;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@userID", userID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string allergy = reader.IsDBNull(0) ? "N/A" : reader.GetString(0);
                    string food = reader.IsDBNull(1) ? "No Match Found" : reader.GetString(1);
                    resultList.Add((allergy, food));
                }
            }
        }

        string[] headers = { "Allergy", "Food Name Match" };
        int[] widths = { 20, 30 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        view.Clear("Allergies and Related Foods");

        Console.WriteLine(pad + new string('-', totalWidth));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(pad);
        for (int i = 0; i < headers.Length; i++)
        {
            Console.Write(headers[i].PadRight(widths[i]) + "   ");
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(pad + new string('-', totalWidth));

        if (resultList.Count == 0)
        {
            Console.WriteLine(pad + "No allergy records found for this user.");
        }
        else
        {
            foreach (var row in resultList)
            {
                string line = string.Format("{0,-20}   {1,-30}", Truncate(row.Allergy, 20), Truncate(row.Food, 30));
                Console.WriteLine(pad + line);
            }
            Console.WriteLine(pad + new string('-', totalWidth));
        }

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(pad + "Press any key to return...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
    public void DietGoalCombos()
    {
        ConsoleView view = new ConsoleView();
        List<(string Goal, string DietPlan, int UsageCount)> resultList = new List<(string, string, int)>();

        string query = @"
    SELECT TOP 30
      G.goal AS [Goal],
      P.dietPlan AS [Diet Plan],
      COUNT(*) AS [No. Of Times Used]
    FROM
      admins.tblDietPlans P, users.tblGoals G
    WHERE
      G.dietPlanID = P.dietPlanID
    GROUP BY
      G.goal, 
      P.dietPlan
    ORDER BY
      COUNT(*) DESC;";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                string goal = reader.GetString(0);
                string dietPlan = reader.GetString(1);
                int count = reader.GetInt32(2);
                resultList.Add((goal, dietPlan, count));
            }
        }

        string[] headers = { "Goal", "Diet Plan", "Times Used" };
        int[] widths = { 25, 25, 12 };
        int totalWidth = widths.Sum() + (3 * (headers.Length - 1));
        int consoleWidth = Console.WindowWidth;
        int leftPad = Math.Max(0, (consoleWidth - totalWidth) / 2);
        string pad = new string(' ', leftPad);

        int pageSize = 20;
        int currentPage = 0;
        int totalPages = (int)Math.Ceiling((double)resultList.Count / pageSize);

        while (true)
        {
            view.Clear("Popular goal and diet combos");

            Console.WriteLine(pad + new string('-', totalWidth));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pad);
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(widths[i]) + "   ");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(pad + new string('-', totalWidth));

            if (resultList.Count == 0)
            {
                Console.WriteLine(pad + "No data found.");
            }
            else
            {
                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, resultList.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var row = resultList[i];
                    string line = string.Format("{0,-25}   {1,-25}   {2,-12}",
                        Truncate(row.Goal, 25),
                        Truncate(row.DietPlan, 25),
                        row.UsageCount);

                    Console.WriteLine(pad + line);
                }

                Console.WriteLine(pad + new string('-', totalWidth));
                Console.WriteLine(pad + $"Page {currentPage + 1} of {Math.Max(totalPages, 1)}. Use ← or → to scroll.");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(pad + "Press any other key to return...");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && currentPage > 0)
            {
                currentPage--;
            }
            else if (key.Key == ConsoleKey.RightArrow && currentPage < totalPages - 1)
            {
                currentPage++;
            }
            else if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
            {
                break;
            }
        }
    }
}