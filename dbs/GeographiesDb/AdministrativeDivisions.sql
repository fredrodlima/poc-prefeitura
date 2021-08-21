CREATE TABLE [dbo].[AdministrativeDivisions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NULL,
	[AdministrativeLevelId] INT NOT NULL,
	[Geography] geography
)