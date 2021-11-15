CREATE PROCEDURE [dbo].[ListarArchivosSubDirectorio]
	@IdDirectorio int
AS
begin
	select top 1
	almacenamiento.VERSION_ARCHIVO as 'VERSION',
	almacenamiento.id_archivo as 'ID_ARCHIVO',
	nombre_archivo as 'NOMBRE',
	extension_extension as 'EXTENSION',
	char_revisionlevel as 'REVISION',
	almacenamiento.id_metadata as 'ID_METADATA'

	from archivos,revisionlevels,extensiones,almacenamiento
	where 
	id_carpeta_padre = @IdDirectorio 
	and activo=1 and
	almacenamiento.ID_ARCHIVO=archivos.id_archivo and
	almacenamiento.ID_EXTENSION=extensiones.id_extension
	order by VERSION desc
end
