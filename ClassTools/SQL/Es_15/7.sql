
--Imposta un vincolo UNIQUE sul nome azienda (CompanyName) dei clienti.

ALTER TABLE Customers ADD CONSTRAINT UQCompanyName UNIQUE (CompanyName)