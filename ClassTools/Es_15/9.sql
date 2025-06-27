--Crea una stored procedure che, dato un ID cliente, restituisca i suoi ordini.
CREATE PROCEDURE returnOrders @CustomerID VARCHAR(255) AS
SELECT * FROM Orders WHERE Orders.OrderID = @CustomerID
