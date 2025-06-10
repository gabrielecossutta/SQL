--Quali sono i prodotti più venduti?

SELECT IdProdotto, SUM(Quantità) as Quantit FROM DettagliOrdini GROUP BY IdProdotto ORDER BY Quantit DESC