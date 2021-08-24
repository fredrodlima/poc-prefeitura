﻿CREATE TABLE [dbo].[Projects]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(100) NOT NULL,
	[SupervisorId] INT NOT NULL,
	[StartDate] DATE NOT NULL,
	[EndDate] DATE NULL
)
