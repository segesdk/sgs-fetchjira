DECLARE @Target TABLE(a int, b varchar(10), c int)
DECLARE @Source TABLE(a int, b varchar(10), c int)
DECLARE @Changes TABLE(change varchar(200))

INSERT INTO @Target VALUES (1,'a',0), (2,'b',0), (3,'c',0)
INSERT INTO @Source VALUES (1,'x',0), (2,'b',1),           (4,'d',0), (5,'e',0) 

MERGE @Target as T
USING (SELECT * FROM @Source) AS S
ON (T.a = S.a)

WHEN MATCHED AND (T.b != S.b OR T.c != S.c) THEN UPDATE SET T.b = S.b, T.c = S.c
WHEN NOT MATCHED BY SOURCE THEN DELETE 
WHEN NOT MATCHED BY TARGET THEN INSERT VALUES (a,b,c)
OUTPUT 
	--$action
	CONCAT(getdate(),';',$action,';',DELETED.a,';',DELETED.b,';',INSERTED.a,';',INSERTED.b) 
	INTO @Changes
;

SELECT * FROM @Target
SELECT * FROM @Changes
--SELECT change, COUNT(*) antal FROM @Changes GROUP BY change
