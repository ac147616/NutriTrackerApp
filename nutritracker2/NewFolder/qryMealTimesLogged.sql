-- Row 1: Total logs per meal
SELECT
  'Total Logs' AS [ ],
  CAST(COUNT(CASE WHEN L.mealTime = 'BREAKFAST' THEN 1 END) AS VARCHAR) AS [Breakfast],
  CAST(COUNT(CASE WHEN L.mealTime = 'LUNCH' THEN 1 END) AS VARCHAR) AS [Lunch],
  CAST(COUNT(CASE WHEN L.mealTime = 'DINNER' THEN 1 END) AS VARCHAR) AS [Dinner],
  CAST(COUNT(*) AS VARCHAR) AS [Total]
FROM
  admins.tblFoods F
JOIN
  users.tblDailyLog L ON F.foodID = L.foodID

UNION ALL

SELECT
  'Most Logged Food' AS [ ],
  MAX(CASE WHEN mealTime = 'BREAKFAST' THEN foodName END) AS [Breakfast],--Max is used to pick non null value and it collapses multiple rows into one.
  MAX(CASE WHEN mealTime = 'LUNCH' THEN foodName END) AS [Lunch],
  MAX(CASE WHEN mealTime = 'DINNER' THEN foodName END) AS [Dinner],
  ' ' AS [Total]
FROM (
  SELECT
    L.mealTime,
    F.foodName,
    COUNT(*) AS occurrences,
    ROW_NUMBER() OVER (PARTITION BY L.mealTime ORDER BY COUNT(*) DESC) AS rowNo -- Ranking within each meal, partition means that the row numbers are resetted for each of the meal times
  FROM
    users.tblDailyLog L, admins.tblFoods F
  WHERE
    F.foodID = L.foodID
  GROUP BY
    L.mealTime, F.foodName
) AS RankedFoods
WHERE rowNo = 1; -- Finding the MOST common food so ranked 1
