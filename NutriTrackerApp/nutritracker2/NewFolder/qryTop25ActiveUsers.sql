SELECT TOP 25
  D.firstName + ' ' + D.lastName AS [Name],
  CONVERT(DECIMAL(10,2), ROUND(AVG(F.calories), 2)) AS [Avg Calories (g)],
  COUNT(*) AS [Total Logs]
FROM
  users.tblUserDetails D, users.tblDailyLog L, admins.tblFoods F
WHERE
   D.userID = L.userID 
   AND F.foodID = L.foodID
GROUP BY
  D.firstName + ' ' + D.lastName
ORDER BY
  [Total Logs] DESC;

