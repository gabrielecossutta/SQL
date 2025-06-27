--Crea una stored procedure che aggiorna automaticamente il prezzo di tutti i prodotti di una categoria aumentandolo del 10%.

CREATE PROCEDURE ProcedureName @Categoria INT AS
BEGIN
UPDATE Products SET UnitPrice = UnitPrice*1.1
WHERE CategoryID = @Categoria
END

EXEC ProcedureName @Categoria = 2