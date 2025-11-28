USE AuditingDB;
GO
CREATE OR ALTER VIEW dbo.vCompletedAuditsSummary
AS
SELECT 
  a.Id,
  a.Title,
  a.AuditedArea,
  a.StartDate,
  a.EndDate,
  o.Name AS OwnerName,
  o.Area AS OwnerArea,
  dbo.CountFindingsBySeverity(a.Id, 0) AS FindingsLow,
  dbo.CountFindingsBySeverity(a.Id, 1) AS FindingsMedium,
  dbo.CountFindingsBySeverity(a.Id, 2) AS FindingsHigh
FROM Audits a
JOIN Owners o ON o.Id = a.OwnerId
WHERE a.Status = 2; -- Completed
GO