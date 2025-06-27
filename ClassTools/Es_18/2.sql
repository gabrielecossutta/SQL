
--Quali utenti hanno preso in prestito più di 3 libri?
SELECT IdUtente FROM Prestiti GROUP BY IdUtente HAVING Count(*) >3

