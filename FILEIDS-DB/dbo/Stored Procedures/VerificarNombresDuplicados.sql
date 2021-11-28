/*
Retorna el ID_ARCHIVO de un archivo con el mismo nombre, 
la misma extensión y la misma carpeta que el suministrado. 
Si tal duplicación no existe, retorna 0.
*/

CREATE PROCEDURE [dbo].[VerificarNombresDuplicados]
	@NombreArchivo varchar (256),
	@Extension varchar(20),
	@IdDirectorioPadre int,
	@IdDirectorioRaiz int
AS
begin
declare @IdArchivoDuplicado int

	select @IdArchivoDuplicado=ARCHIVOS.ID_ARCHIVO 
	from ARCHIVOS,ALMACENAMIENTO,EXTENSIONES
	WHERE ARCHIVOS.NOMBRE_ARCHIVO=@NombreArchivo and
	@Extension=EXTENSIONES.EXTENSION_EXTENSION AND
	@IdDirectorioPadre=ARCHIVOS.ID_CARPETA_PADRE AND
	ALMACENAMIENTO.ID_EXTENSION=EXTENSIONES.ID_EXTENSION AND
	ARCHIVOS.ID_ARCHIVO=ALMACENAMIENTO.ID_ARCHIVO
	
	set @IdArchivoDuplicado=isnull(@IdArchivoDuplicado,0)
	select @IdArchivoDuplicado
end
