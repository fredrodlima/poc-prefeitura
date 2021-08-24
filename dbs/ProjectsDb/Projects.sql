﻿CREATE TABLE [dbo].[Projects]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(100) NOT NULL,
	[SupervisorId] INT NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NULL
)
