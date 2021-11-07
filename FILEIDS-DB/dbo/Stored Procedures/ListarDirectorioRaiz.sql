--Listar todos los directorios raiz.
CREATE PROCEDURE [dbo].[ListarDirectorioRaiz]

AS
	SELECT 
	id_directorio as 'ID',
	nombre_directorio as 'DIRECTORIO',
	descriptor_directorio as 'DESCRIPCIÓN',
	activo as 'Es activo'
	from directorios
	where id_padre is null
RETURN 0
