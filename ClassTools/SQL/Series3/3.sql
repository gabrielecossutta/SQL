
CREATE PROCEDURE NomeProcedura12 @Val INT
AS
BEGIN

SELECT TOP 5 * FROM Products WHERE SupplierID = @Val  Order BY UnitPrice Desc 

END

EXECUTE NomeProcedura12 @val = 1;
------------Scrivi una stored procedure che restituisce i 5 prodotti più costosi venduti da un determinato fornitore.

.