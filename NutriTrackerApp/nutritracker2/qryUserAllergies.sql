DECLARE @userID AS INTEGER = 550  -- a number between 1 - 10001 will show allergies of a specific user.

SELECT A.allergy, F.foodName
FROM users.tblAllergies A
LEFT JOIN admins.tblFoods F ON F.foodName LIKE CONCAT('%', A.allergy, '%') -- I used this join becuase I want all the allergies ot show even if they dont have a related food.
WHERE A.userID = @userID; 