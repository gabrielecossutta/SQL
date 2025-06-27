--Mostra tutti i prodotti che non hanno un nome specificato (NULL o vuoto).

SELECT * FROM Products WHERE ProductName IN (NULL,'')