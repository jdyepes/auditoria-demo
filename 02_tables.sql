USE AuditingDB;
GO

-- Reponsable
CREATE TABLE Owners (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name NVARCHAR(150) NOT NULL,         -- Nombre del responsable
  Email NVARCHAR(150) NOT NULL UNIQUE, -- Correo del responsable (único)
  Area NVARCHAR(100) NOT NULL,         -- Área del responsable
  CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME() -- Creación UTC
);

-- Auditoria
CREATE TABLE Audits (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Title NVARCHAR(200) NOT NULL,         -- Título de la auditoría
  StartDate DATE NOT NULL,              -- Fecha de inicio
  EndDate DATE NOT NULL,                -- Fecha de fin
  AuditedArea NVARCHAR(150) NOT NULL,   -- Área auditada
  OwnerId INT NOT NULL,                 -- Responsable asignado (FK)
  Status INT NOT NULL DEFAULT 0,        -- 0 Pending, 1 InProgress, 2 Completed
  CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), -- Creación UTC
  UpdatedAtUtc DATETIME2 NULL,          -- Última actualización UTC
  CONSTRAINT FK_Audits_Owners FOREIGN KEY (OwnerId) REFERENCES Owners(Id)
);

-- Hallazgos
CREATE TABLE Findings (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  AuditId INT NOT NULL,                 -- FK de auditoría
  Description NVARCHAR(500) NOT NULL,   -- Descripción del hallazgo
  Type INT NOT NULL,                    -- 0 Observation, 1 NonConformity
  Severity INT NOT NULL,                -- 0 Low, 1 Medium, 2 High
  DetectionDate DATE NOT NULL,          -- Fecha de detección
  CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), -- Creación UTC
  CONSTRAINT FK_Findings_Audits FOREIGN KEY (AuditId) REFERENCES Audits(Id)
);

CREATE INDEX IX_Audits_DateStatus ON Audits (StartDate, EndDate, Status);
CREATE INDEX IX_Findings_AuditSeverity ON Findings (AuditId, Severity);
CREATE INDEX IX_Audits_Owner ON Audits (OwnerId);