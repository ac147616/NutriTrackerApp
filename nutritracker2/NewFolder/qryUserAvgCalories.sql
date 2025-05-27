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
  [Avg Calories Per log (g)];
