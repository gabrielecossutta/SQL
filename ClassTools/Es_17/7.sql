--Usa un cursore per stampare il nome di ogni cliente che ha almeno un ordine.

DECLARE @Iddd NCHAR(5)
DECLARE CursorName CURSOR FOR SELECT CustomerID FROM Customers

OPEN CursorName
FETCH NEXT FROM CursorName INTO @Iddd 
WHILE @@FETCH_STATUS=0
BEGIN 
Print 'ID' + CAST(@Iddd AS VARCHAR)
FETCH NEXT FROM CursorName INTO @iddd
END
CLOSE CursorName