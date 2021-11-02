CREATE PROCEDURE [dbo].[DesactivarDirectorioRaiz]
	@IdDirectorio int
AS
	update directorios set activo=0 where id_directorio=@IdDirectorio
RETURN 0
