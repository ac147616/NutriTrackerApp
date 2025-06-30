SELECT TOP 30
  G.goal AS [Goal],
  P.dietPlan AS [Diet Plan],
  COUNT(*) AS [No. Of Times Used]
FROM
  admins.tblDietPlans P, users.tblGoals G
WHERE
  G.dietPlanID = P.dietPlanID
GROUP BY
  G.goal, 
  P.dietPlan
ORDER BY
  COUNT(*) DESC;
