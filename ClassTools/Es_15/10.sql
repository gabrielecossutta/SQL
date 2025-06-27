--Crea una funzione che calcoli uno sconto del 10% su un importo superiore a 100.

CREATE FUNCTION NomeFunzione (@Price DECIMAL(10,2)) RETURNS DECIMAL(10,2)
AS 
BEGIN 
	DECLARE @FinalPrice DECIMAL(10,2)
	SET @FinalPrice = CASE WHEN @Price > 100 THEN @Price * 0.9  ELSE @Price END
	RETURN @FinalPrice
END

