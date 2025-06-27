--Elenca tutti i clienti e i relativi ordini, anche se non hanno effettuato ordini (LEFT JOIN).

SELECT * FROM Customers LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID