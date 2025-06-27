--Mostra tutti i prodotti e, se non hanno un nome, visualizza 'Sconosciuto' al suo posto.

SELECT 
(CASE
	WHEN ProductName = NULL or ProductName = '' THEN 'Sconosciuto' else ProductName
END)
FROM Products
