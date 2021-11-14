/*
Obtiene la metadata de un archivo. Si no se suministra una versión, se retorna una consulta con todos los datos de todas las versiones.
*/
CREATE PROCEDURE [dbo].[GetMetadata]
	@IdArchivo int,
	@VersionArchivo int=0
AS
	if exists(select id_archivo from archivos where id_archivo=@IdArchivo)
	begin
		if @VersionArchivo=0
		begin
			select almacenamiento.id_metadata as 'id_metadata',
			version_archivo,
			nombre_archivo,
			descriptores,
			descriptoren,
			oemsku,
			descriptorextra,
			partid  
			from metadata,almacenamiento,archivos where 
			almacenamiento.id_metadata=metadata.id_metadata and 
			almacenamiento.id_archivo=@IdArchivo and 
			archivos.id_archivo=almacenamiento.id_archivo and
			activo=1
		end
		else
		begin
			select almacenamiento.id_metadata as 'id_metadata',
			version_archivo,
			nombre_archivo,
			descriptores,
			descriptoren,
			oemsku,
			descriptorextra,
			partid  
			from metadata,almacenamiento,archivos where 
			almacenamiento.id_metadata=metadata.id_metadata and 
			almacenamiento.id_archivo=@IdArchivo and 
			archivos.id_archivo=almacenamiento.id_archivo and
			activo=1 and
			almacenamiento.version_archivo=@VersionArchivo
		end
	end
	
RETURN 0
