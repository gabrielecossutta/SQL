CREATE TRIGGER NomeTrigger 
ON Customers 
INSTEAD OF DELETE 
AS 
BEGIN
BEGIN 
IF EXISTS(SELECT COUNT(*) FROM Orders JOIN deleted ON Orders.CustomerID = deleted.CustomerID WHERE Orders.CustomerID = deleted.CustomerID)
ROLLBACK 
ELSE
DELETE FROM Customers WHERE CustomerID IN (SELECT CustomerID FROM deleted)
END
--Crea un trigger INSTEAD OF DELETE sulla tabella Customers che impedisca la cancellazione se il cliente ha ordini.
