--Quali autori hanno scritto più libri?
SELECT DISTINCT IdAutore, COUNT(IdLibro) as NumLibri FROM ScrittoDA GROUP BY IdAutore ORDER BY NumLibri DESC
