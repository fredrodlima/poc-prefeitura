﻿CREATE TABLE [dbo].[AdministrativeDivisions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NULL,
	[AdministrativeDivisionLevelId] INT NOT NULL,
	[Geography] geography
)