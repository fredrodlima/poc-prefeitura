CREATE TABLE [dbo].[IndividualMetrics]
(
	[ProjectId] INT NOT NULL PRIMARY KEY,
	[PhasesNotStarted] INT NOT NULL,
	[PhasesInProgress] INT NOT NULL,
	[PhasesCompleted] INT NOT NULL,
	[Progress] FLOAT NOT NULL
)
