DECLARE @userID AS INTEGER = 16  -- a number between 1 - 10001 will show goals for a specific user.

SELECT G.goalID AS [ID], G.goal AS [Goal], G.dateStarted AS [Started On (yyyy/mm/dd)], G.dateEnded AS [Ended On (yyyy/mm/dd)]
FROM users.tblGoals G
WHERE G.userID = @userID;
