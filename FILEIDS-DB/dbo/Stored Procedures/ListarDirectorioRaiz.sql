CREATE PROCEDURE [dbo].[ListarDirectorioRaiz]

AS
	SELECT 
	id_directorio as 'ID',
	nombre_directorio as 'DIRECTORIO',
	descriptor_directorio as 'DESCRIPCIÓN',
	activo as 'Es activo'
	from directorios
RETURN 0
