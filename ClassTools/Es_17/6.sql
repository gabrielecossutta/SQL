-- Scrivi uno script che usa una transazione per inserire un ordine e i suoi dettagli, con rollback in caso di errore.

BEGIN TRY
BEGIN TRANSACTION
INSERT INTO Orders (CustomerID,RequiredDate) VALUES(0,'ciao')
COMMIT TRANSACTION
END TRY
BEGIN CATCH
PRINT 'ERRORE'
ROLLBACK TRANSACTION
END CATCH
