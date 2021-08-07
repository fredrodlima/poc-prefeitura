CREATE TABLE [dbo].[Locations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NULL,
	[Geography] geography,
	[GeographyText] AS [Geography].STAsText()
)