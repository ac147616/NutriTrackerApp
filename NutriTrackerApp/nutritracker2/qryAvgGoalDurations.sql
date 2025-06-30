SELECT
  G.goalID AS [ID],
  G.goal AS [Goal],
  AVG(DATEDIFF(DAY, G.dateStarted, G.dateEnded)) AS [Avg Duration (days)]
FROM
  users.tblGoals G
WHERE
  G.dateStarted IS NOT NULL 
  AND G.dateEnded IS NOT NULL
GROUP BY
  G.goal,
  G.goalID
ORDER BY
  [Avg Duration (days)];
