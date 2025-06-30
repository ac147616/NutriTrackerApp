-- This query ussed weight/height squared to get body mass index then I used the range of values and what they indicate from google to determine teh status.

SELECT 
    (D.firstName + ' ' + D.lastName) AS [User], 
    CONVERT(DECIMAL(10,2), D.userWeight/((D.userHeight/100) * (D.userHeight/100)), 2) AS [BMI],
    CASE
        WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) < 18.5 THEN 'Underweight'
        WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) BETWEEN 18.5 AND 24.9 THEN 'Normal weight'
        WHEN D.userWeight / ((D.userHeight/100) * (D.userHeight/100)) BETWEEN 25.0 AND 29.9 THEN 'Overweight'
        ELSE 'Obese'
    END AS [Status]
FROM users.tblUserDetails D;