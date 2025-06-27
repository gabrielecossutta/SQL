
--Crea un trigger che registri in una tabella di log ogni nuovo ordine inserito.

CREATE TABLE LogMessage(
LogID INT IDENTITY PRIMARY KEY,
Message NVARCHAR(255)
);

go
CREATE TRIGGER NomesTrigger 
ON Orders 
AFTER INSERT
AS 
BEGIN 
INSERT INTO LogMessage (Message)VALUES('ciao')
END
