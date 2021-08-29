CREATE TABLE [dbo].[GlobalMetrics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PhasesNotStarted] INT NOT NULL,
	[PhasesInProgress] INT NOT NULL,
	[PhasesCompleted] INT NOT NULL,
	[Progress] FLOAT NOT NULL
)
