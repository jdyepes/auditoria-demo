USE AuditingDB;
GO
IF NOT EXISTS (SELECT 1 FROM Owners WHERE Email = 'owner@company.com')
INSERT INTO Owners (Name, Email, Area) VALUES ('Primary Owner', 'owner@company.com', 'Quality');

DECLARE @OwnerId INT = (SELECT Id FROM Owners WHERE Email = 'owner@company.com');
INSERT INTO Audits (Title, StartDate, EndDate, AuditedArea, OwnerId, Status)
VALUES ('Quality Audit Q4', '2025-10-01', '2025-10-15', 'Plant Caracas', @OwnerId, 2);

DECLARE @AuditId INT = SCOPE_IDENTITY();

INSERT INTO Findings (AuditId, Description, Type, Severity, DetectionDate)
VALUES 
(@AuditId, N'Unsigned procedure', 1, 2, '2025-10-05'),
(@AuditId, N'Incomplete checklist', 0, 1, '2025-10-06');