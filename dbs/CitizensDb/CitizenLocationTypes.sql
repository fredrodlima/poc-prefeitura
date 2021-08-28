CREATE TABLE [dbo].[CitizenLocationTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CitizenTypeId] INT NOT NULL,
	[Name] NVARCHAR(100)
)
