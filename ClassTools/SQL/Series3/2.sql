
SELECT DISTINCT COUNT(*)OVER(PARTITION BY CustomerID), COUNT(*)OVER() FROM Orders

--Utilizza una funzione di finestra per ottenere il totale degli ordini per cliente insieme al totale generale per ogni riga.
