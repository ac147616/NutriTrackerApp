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
  [No. Times Logged] DESC;
