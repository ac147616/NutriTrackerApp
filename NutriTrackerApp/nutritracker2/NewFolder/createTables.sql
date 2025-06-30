--I added this incase the tables are created incorrectly or theres some other error. You wont have to delete tables seperately - just run this file again.
DROP TABLE IF EXISTS users.tblDailyLog;
DROP TABLE IF EXISTS users.tblGoals;
DROP TABLE IF EXISTS users.tblAllergies;
DROP TABLE IF EXISTS users.tblUserDetails;

DROP TABLE IF EXISTS admins.tblDietPlans;
DROP TABLE IF EXISTS admins.tblFoods;
DROP TABLE IF EXISTS admins.tblAdminDetails;

DROP SCHEMA IF EXISTS users;
DROP SCHEMA IF EXISTS admins;
GO

-- Create 2 Schemas
CREATE SCHEMA users; 
GO
CREATE SCHEMA admins; 
GO


-- Create 7 Tables
CREATE TABLE users.tblUserDetails (
	userID INT IDENTITY (1, 1) PRIMARY KEY,
	firstName VARCHAR (255) NOT NULL,
	lastName VARCHAR (255) NOT NULL,
	emailID VARCHAR (255) NOT NULL,
	passwordkey VARCHAR (100) NOT NULL,
	age INT check (age between 0 and 100),
	gender VARCHAR (225),
	CONSTRAINT chk_gender CHECK (UPPER(gender) IN ('MALE', 'FEMALE', 'NULL')),
	userWeight DECIMAL(5, 2),
	userHeight DECIMAL(5, 2),
	signUpDate DATE,
	);

CREATE TABLE users.tblAllergies (
	allergyID INT IDENTITY (1, 1) PRIMARY KEY,
	userID int FOREIGN KEY REFERENCES users.tblUserDetails(userID),
	allergy VARCHAR (255) NOT NULL
	);

CREATE TABLE admins.tblAdminDetails (
	adminID INT IDENTITY (1, 1) PRIMARY KEY,
	firstName VARCHAR (255) NOT NULL,
	lastName VARCHAR (255) NOT NULL,
	emailID VARCHAR (255) NOT NULL,
	passwordkey VARCHAR (100) NOT NULL,								
	);

CREATE TABLE admins.tblFoods (
	foodID INT IDENTITY (1, 1) PRIMARY KEY,
	foodName VARCHAR (255) NOT NULL,
	category VARCHAR (255) NOT NULL,
	calories DECIMAL(5, 2),
	carbohydrates DECIMAL(5, 2),
	proteins DECIMAL(5, 2),
	fats DECIMAL(5, 2),
	servingSize DECIMAL(5, 2)
	);

CREATE TABLE admins.tblDietPlans (
	dietPlanID INT IDENTITY (1, 1) PRIMARY KEY,
	dietPlan VARCHAR (255) NOT NULL,
	caloriesTarget INT NOT NULL,
	proteinsTarget INT NOT NULL,
	carbohydratesTarget INT NOT NULL,
	fatsTarget INT NOT NULL
	);

CREATE TABLE users.tblGoals (
	goalID INT IDENTITY (1, 1) PRIMARY KEY,
	userID INT FOREIGN KEY REFERENCES users.tblUserDetails(userID),
	dietPlanID INT FOREIGN KEY REFERENCES admins.tblDietPlans(dietPlanID),
	goal VARCHAR (255) NOT NULL,
	dateStarted DATE,
	dateEnded DATE
	);

CREATE TABLE users.tblDailyLog (
	logID INT IDENTITY (1, 1) PRIMARY KEY,
	userID int FOREIGN KEY REFERENCES users.tblUserDetails(userID),
	foodID int FOREIGN KEY REFERENCES admins.tblFoods(foodID),
	mealTime VARCHAR (225),
	CONSTRAINT chk_mealTime CHECK (UPPER(mealTime) IN ('BREAKFAST', 'LUNCH', 'DINNER')),
	dateLogged DATE
	);