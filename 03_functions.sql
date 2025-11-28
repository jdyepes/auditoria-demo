USE AuditingDB;
GO
CREATE OR ALTER FUNCTION dbo.CountFindingsBySeverity (@AuditId INT, @Severity INT)
RETURNS INT
AS
BEGIN
  DECLARE @Count INT;
  SELECT @Count = COUNT(*) FROM Findings WHERE AuditId = @AuditId AND Severity = @Severity;
  RETURN ISNULL(@Count, 0);
END;
GO