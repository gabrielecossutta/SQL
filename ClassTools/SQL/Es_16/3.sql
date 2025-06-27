SELECT P.ProductName,
p.UnitPrice
FROM Products p 
WHERE p.UnitPrice > (SELECT AVG(UnitPrice) FROM Products)

--Elenca i prodotti con un prezzo superiore alla media di tutti i prodotti.



--Inserisci un nuovo cliente fittizio nella tabella Customers.
--Aggiorna il nome dell'azienda del cliente appena inserito.
--Elimina il cliente fittizio appena inserito.
--Crea un indice sul campo CompanyName della tabella Customers per velocizzare le ricerche.
