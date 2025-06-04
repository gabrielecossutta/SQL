--Crea una vista che mostri per ogni ordine il totale, ma includa anche quelli senza dettagli (utilizza COALESCE).

CREATE VIEW ViewName12 AS 
SELECT OrderID,ProductID, COALESCE(SUM(Quantity*UnitPrice),0)  as ColonnaName
FROM [Order Details]
group by OrderID,ProductID
