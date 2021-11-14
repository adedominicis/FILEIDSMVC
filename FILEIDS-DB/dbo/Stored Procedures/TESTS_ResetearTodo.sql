CREATE PROCEDURE [dbo].[TESTS_ResetearTodo]

AS
	
	delete from almacenamiento
	delete from metadata
	delete from archivos
	delete from directorios
	delete from revisionlevels
	DBCC CHECKIDENT ('almacenamiento', RESEED, 0)
	DBCC CHECKIDENT ('metadata', RESEED, 0)
	DBCC CHECKIDENT ('archivos', RESEED, 0)
	DBCC CHECKIDENT ('directorios', RESEED, 0)
	DBCC CHECKIDENT ('revisionlevels', RESEED, 0)
	insert into revisionlevels values('-')
	DBCC CHECKIDENT ('almacenamiento')
	DBCC CHECKIDENT ('metadata')
	DBCC CHECKIDENT ('archivos')
	DBCC CHECKIDENT ('directorios')
	DBCC CHECKIDENT ('revisionlevels')

RETURN 0
