INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])  
VALUES ('Bom destino', 1, geography::STGeomFromText('POLYGON((-43.900 -19.900, -41.900 -19.900, -41.900 -17.900, -43.900 -17.900, -43.900 -19.900))', 4326));
GO 

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Região Sudeste', 2, geography::STGeomFromText('POLYGON((-43.900 -19.900, -42.900 -19.900, -42.900 -18.900, -43.900 -18.900, -43.900 -19.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Região Sudoeste', 2, geography::STGeomFromText('POLYGON((-42.900 -19.900 , -41.900 -19.900, -41.900 -18.900, -42.900 -18.900, -42.900 -19.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Região Nordeste', 2, geography::STGeomFromText('POLYGON((-43.900 -18.900 , -42.900 -18.900, -42.900 -17.900, -43.900 -17.900, -43.900 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Região Noroeste', 2 , geography::STGeomFromText('POLYGON((-42.900 -18.900 , -41.900 -18.900, -41.900 -17.900, -42.900 -17.900, -42.900 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NO-01', 3, geography::STGeomFromText('POLYGON((-42.400 -17.900 , -42.900 -17.900, -42.900 -18.400, -42.400 -18.400, -42.400 -17.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NO-02', 3, geography::STGeomFromText('POLYGON((-41.900 -17.900 , -42.400 -17.900, -42.400 -18.400, -41.900 -18.400, -41.900 -17.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NO-03', 3, geography::STGeomFromText('POLYGON((-42.400 -18.400 , -42.900 -18.400, -42.900 -18.900, -42.400 -18.900, -42.400 -18.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NO-04', 3, geography::STGeomFromText('POLYGON((-41.900 -18.400 , -42.400 -18.400, -42.400 -18.900, -41.900 -18.900, -41.900 -18.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NE-01', 3, geography::STGeomFromText('POLYGON((-43.400 -17.900 , -43.900 -17.900, -43.900 -18.400, -43.400 -18.400, -43.400 -17.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NE-02', 3, geography::STGeomFromText('POLYGON((-42.900 -17.900 , -43.400 -17.900, -43.400 -18.400, -42.900 -18.400, -42.900 -17.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NE-03', 3, geography::STGeomFromText('POLYGON((-43.400 -18.400 , -43.900 -18.400, -43.900 -18.900, -43.400 -18.900, -43.400 -18.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro NE-04', 3, geography::STGeomFromText('POLYGON((-42.900 -18.400 , -43.400 -18.400, -43.400 -18.900, -42.900 -18.900, -42.900 -18.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SO-01', 3, geography::STGeomFromText('POLYGON((-42.400 -18.900 , -42.900 -18.900, -42.900 -19.400, -42.400 -19.400, -42.400 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SO-02', 3, geography::STGeomFromText('POLYGON((-41.900 -18.900 , -42.400 -18.900, -42.400 -19.400, -41.900 -19.400, -41.900 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SO-03', 3, geography::STGeomFromText('POLYGON((-42.400 -19.400 , -42.900 -19.400, -42.900 -19.900, -42.400 -19.900, -42.400 -19.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SO-04', 3, geography::STGeomFromText('POLYGON((-41.900 -19.400 , -42.400 -19.400, -42.400 -19.900, -41.900 -19.900, -41.900 -19.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SE-01', 3, geography::STGeomFromText('POLYGON((-43.400 -18.900 , -43.900 -18.900, -43.900 -19.400, -43.400 -19.400, -43.400 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SE-02', 3, geography::STGeomFromText('POLYGON((-42.900 -18.900 , -43.400 -18.900, -43.400 -19.400, -42.900 -19.400, -42.900 -18.900))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SE-03', 3, geography::STGeomFromText('POLYGON((-43.400 -19.400 , -43.900 -19.400, -43.900 -19.900, -43.400 -19.900, -43.400 -19.400))', 4326));  
GO

INSERT INTO [dbo].[AdministrativeDivisions] ([Name], [AdministrativeDivisionLevelId], [Geography])
VALUES ('Bairro SE-04', 3, geography::STGeomFromText('POLYGON((-42.900 -19.400 , -43.400 -19.400, -43.400 -19.900, -42.900 -19.900, -42.900 -19.400))', 4326));  
GO