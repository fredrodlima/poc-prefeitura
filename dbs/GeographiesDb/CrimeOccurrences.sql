CREATE TABLE [dbo].[CrimeOccurrences]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Description] NVARCHAR(100) NOT NULL,
	[PoliceDepartmentId] INT NOT NULL,
	[PoliceReportId] INT NOT NULL,
	[CrimeTypeId] INT NOT NULL,
	[Location] geography NOT NULL
)