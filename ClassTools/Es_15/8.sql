CREATE VIEW CustomView AS 
SELECT [Order Details].OrderID as details_orderID, Orders.OrderID  as order_ordersID
FROM [Order Details] 
INNER JOIN Orders on [Order Details].OrderID = Orders.OrderID 

