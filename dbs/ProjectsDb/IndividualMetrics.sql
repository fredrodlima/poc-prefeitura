CREATE TABLE [dbo].[IndividualMetrics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProjectId] INT NOT NULL,
	[PhasesNotStarted] INT NOT NULL,
	[PhasesInProgress] INT NOT NULL,
	[PhasesCompleted] INT NOT NULL,
	[Progress] FLOAT NOT NULL
)
