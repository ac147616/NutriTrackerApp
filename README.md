# NutriTrackerApp

NutriTrackerApp is a C# console-based application designed to help users track their food intake, monitor nutrition, set diet goals, and view statistics. It is backed by a SQL Server database and supports both User and Admin roles. The goal of this application is to provide a free, accessible alternative to expensive or premium-only health tracking tools.

---

## Features

### For Users
- View nutritional details of foods
- Log daily meals
- Set and manage dietary goals
- Track consistency across meals and diet plans
- View summaries of nutrition performance and other reports

### For Admins
- Manage nutritional information such as diets and foods
- Manage user and admin accounts
- Making sure logic and structure within app works correctly

---

## How It Works

1. **Start the App**
   - Launch the console application.
   - Choose to either log in as a **New User**, **Existing User** or **Admin**.

2. **Menu Navigation**
   - Use **arrow keys (↑ ↓)** to navigate options.
   - Press **Enter** to select.
   - Use **Ctrl + B** to go back to the main menu.
   - Use **Ctrl + H** to view the help page.
   - Use **Ctrl + E** to exit the application.

3. **View of Multiple Pages**
   - Many sections display results in pages (because it can only display 20 records per page).
   - Use **←** and **→** keys to navigate between pages and sometimes even Up Arrow for category's.
   - If no more pages exist, nothing should happen if you press the arrows.

4. **Forms and Input**
   - Forms will prompt you to enter specific values.
   - All entries are validated. If a mistake is made (e.g., typing "a" instead of a number), a helpful message will appear.

5. **Data Management**
   - All data is stored in a SQL Server database.
   - Tables include: `tblUserDetails`, `tblGoals`, `tblAllergies`, `tblDailyLog`, `tblFoods`, and `tblDietPlans`.

---

## Requirements

### Software
- [.NET 6 or higher](https://dotnet.microsoft.com/download)
- [SQL Server Express or LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- Visual Studio 2022 (or any compatible IDE)

### Database
- Ensure your connection string in the code points to the correct database instance.
- The database must include all required tables and seed data.

---

## Help & Usage Tips

- **Full Screen Recommended:** For best results and layout formatting, run the app in full screen. Console layouts may misalign in small windows.

---

## Future Improvements

- Convert console app into a web-based ASP.NET Core application
- Improve accessibility features
- Generate personalized meal recommendations

---

## Author

Developed by Annika Singh as a part of a school project 
Guided by: Mr. Kucio  
Repository: [https://github.com/ac147616/NutriTrackerApp](https://github.com/ac147616/NutriTrackerApp)

---

## Disclaimer

This project is developed for educational purposes and is not intended to be used to be in place of a professional doctor or dietitian.
