CREATE TABLE [dbo].[CitizenLocalities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	[CitizenId] INT NOT NULL,
	[LocationTypeId] INT NOT NULL,
	[Location] geography NOT NULL
)