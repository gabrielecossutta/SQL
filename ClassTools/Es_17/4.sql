CREATE FUNCTION NomeFunzione(@Val INT) 
RETURNS INT
AS 
BEGIN 
	DECLARE @TotOrdini INT
	SELECT @TotOrdini = SUM(Quantity)
	FROM [Order Details]
	Where OrderID = @Val
	RETURN @TotOrdini
END
--Crea una funzione che accetti un ID ordine e restituisca il numero totale di prodotti contenuti.
