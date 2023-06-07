
BEGIN TRANSACTION

DECLARE @Changes TABLE(change varchar(200))

MERGE timer as T
USING (SELECT * FROM timerupdate) AS S
ON (T.id = S.id)

--WHEN MATCHED AND (T.b != S.b OR T.c != S.c) THEN UPDATE SET T.b = S.b, T.c = S.c
WHEN MATCHED THEN UPDATE SET 
	  T.[username]             = S.[username]                 ,
      T.[email]				   = S.[email]					  ,
      T.[timeSpentSeconds]	   = S.[timeSpentSeconds]		  ,
      T.[issueId]			   = S.[issueId]				  ,
      T.[issueKey]			   = S.[issueKey]				  ,
      T.[started]			   = S.[started]				  ,
      T.[id]				   = S.[id]						  ,
      T.[caseNumber]		   = S.[caseNumber]				  ,
      T.[taskNumber]		   = S.[taskNumber]				  ,
      T.[summary]			   = S.[summary]				  ,
      T.[labels]			   = S.[labels]					  ,
      T.[projectkey]		   = S.[projectkey]				  ,
      T.[projectname]		   = S.[projectname]			  ,
      T.[parentissuekey]	   = S.[parentissuekey]			  ,
      T.[parentissuetypename]  = S.[parentissuetypename] 	  ,
      T.[parentissuesummary]   = S.[parentissuesummary]		  ,
      T.[createdatetime]	   = S.[createdatetime]

--WHEN NOT MATCHED BY SOURCE THEN DELETE 
WHEN NOT MATCHED BY TARGET THEN INSERT VALUES ([username],[email],[timeSpentSeconds],[issueId],[issueKey],[started],[id],[caseNumber],[taskNumber],[summary],[labels],[projectkey],[projectname],[parentissuekey],[parentissuetypename],[parentissuesummary],[createdatetime])
OUTPUT 
	--$action
	CONCAT(getdate(),';',$action,';',DELETED.id,';',INSERTED.id) 
	INTO @Changes
;

SELECT * FROM @Changes
--SELECT change, COUNT(*) antal FROM @Changes GROUP BY change

ROLLBACK TRANSACTION