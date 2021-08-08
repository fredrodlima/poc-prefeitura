INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Assalto a mão armada', 1, 1, 1, geography::STGeomFromText('POINT(-122.510 47.510)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Furto de veículo', 1, 2, 1, geography::STGeomFromText('POINT(-122.520 47.520)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Assalto ao Armazén Dia', 2, 1, 1, geography::STGeomFromText('POINT(-122.550 47.550)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Falsidade ideológica', 2, 2, 2, geography::STGeomFromText('POINT(-123.550 47.550)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Formação de quartel', 3, 1, 3, geography::STGeomFromText('POINT(-122.560 47.560)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Assalto ao Armazén Dia',4, 1, 1, geography::STGeomFromText('POINT(-123.560 47.560)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Fraude',4, 2, 4, geography::STGeomFromText('POINT(-123.700 47.700)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Roubo de insumos',5, 1, 1, geography::STGeomFromText('POINT(-124.500 47.500)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Roubo de galinhas',5, 2, 1, geography::STGeomFromText('POINT(-123.500 48.500)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Roubo de insumos',6, 1, 1, geography::STGeomFromText('POINT(-124.500 47.500)', 4326));  
GO

INSERT INTO [dbo].[CrimeOccurrences] ([Description],
	[PoliceDepartmentId],
	[PoliceReportId],
	[CrimeTypeId],
	[Location])
VALUES ('Roubo de materiais', 6, 2, 1, geography::STGeomFromText('POINT(-123.500 48.500)', 4326));  
GO