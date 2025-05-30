--Scrivi una query che mostra il cliente con il maggiore numero di ordini, usando TOP 1 e ORDER BY.

SELECT TOP(1)CustomerID,Count(*) FROM Orders 
GROUP BY CustomerID
ORDER BY COUNT(*) desc
