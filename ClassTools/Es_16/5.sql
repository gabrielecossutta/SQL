--Usa CASE WHEN per indicare "Economico", "Medio" o "Caro" in base al prezzo del prodotto.

SELECT CASE 
	WHEN UnitPrice <= 10 then 'Economico'
		WHEN UnitPrice >10 AND UnitPrice  <50 then 'Medio'
			WHEN UnitPrice >= 50 then 'Caro'
	END

	FROM Products