SELECT * FROM Orders o RIGht JOIN Products  p ON p.ProductID = o.OrderID WHERE p.ProductID NOT IN (o.OrderID)


--Elenca tutti i prodotti che non sono stati mai ordinati (utilizza RIGHT JOIN o subquery).
