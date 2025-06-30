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
  L.mealTime;
