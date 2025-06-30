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
  D.userID = L.userID
  AND F.foodID = L.foodID
GROUP BY
  P.dietPlanID, 
  P.dietPlan, 
  P.caloriesTarget, 
  P.carbohydratesTarget, 
  P.proteinsTarget, 
  P.fatsTarget
ORDER BY
  [Overall Target Met] DESC;