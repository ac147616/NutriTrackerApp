DECLARE @userID AS INTEGER = 0  -- 0 shows all the users but if you give it a number between 1 - 10001 then it will show record for a specific user.

SELECT U.userID AS [ID], U.firstName AS [First Name], U.lastName AS [Last Name], U.emailID AS [Email], U.passwordkey AS [Password], U.age AS [Age], U.gender AS [Gender], U.userWeight AS [Weight (kg)], U.userHeight AS [Height (cm)], U.signUpDate AS [Date Joined (yyyy/mm/dd)]
FROM users.tblUserDetails U
WHERE @userID = 0 OR userID = @userID;

