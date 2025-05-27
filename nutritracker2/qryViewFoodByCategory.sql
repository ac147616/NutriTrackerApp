DECLARE @category AS VARCHAR(255) = 'Processed'  -- '' shows all the foods irrespective of their category but you can also specifiy and see records of a specifc category e.g legume, vegetable, fruit, snack, sweet, processed, cooked meal. More in table's insert statement.

SELECT F.foodID AS [ID], F.foodName AS [Name], F.calories [Calories (g)], F.carbohydrates AS [Carbohydrates (g)], F.proteins AS [Proteins (g)], F.fats AS [Fats (g)], F.servingSize AS [Serving Size (g)]
FROM admins.tblFoods F
WHERE @category = '' OR F.category = @category;