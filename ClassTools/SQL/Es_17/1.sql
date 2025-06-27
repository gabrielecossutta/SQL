WITH NomeTabella AS (SELECT *, RANK()OVER (ORDER BY UnitPrice DESC)as NUMERO FROM Products )

SELECT * FROM NomeTabella



--Usa una CTE per elencare i prodotti e assegnare un numero progressivo in base al prezzo decrescente.

