﻿/*
La actualización de la metadata crea una nueva version del archivo pero no añade nuevo almacenamiento.
Se crea una nueva version en almacenamiento y metadata que van a la misma ruta.
No se crean ni se actualizan partids en este procedimiento, puesto que debe haber un módulo dedicado a eso.
*/
CREATE PROCEDURE [dbo].[ActualizarMetadata]
	@IdArchivo int,
	@VersionArchivo int,
	@DescriptorEs varchar(500),
	@DescriptorEn varchar(500),
	@Oemsku varchar(500),
	@DescriptorExtra varchar(500),
	@CrearNuevaVersion bit=1
	
AS
begin

declare @IdExtension int
declare @IdMetadata int
declare @RutaAlmacenamiento varchar(256)
declare @IdAlmacenamiento int

	begin transaction T1
		begin try

			-- Obtener extension del archivo
				set @IdExtension=(select extensiones.id_extension from extensiones,almacenamiento 
				where extensiones.id_extension=almacenamiento.id_extension and id_archivo=@IdArchivo and version_archivo=@VersionArchivo)

			if @CrearNuevaVersion=1
			begin
				-- Crear nueva version de metadatos.	
				insert into metadata(descriptores,descriptoren,oemsku,descriptorextra,partid)
				values (@DescriptorEs,@DescriptorEn,@Oemsku,@Descriptorextra,null);
				set @IdMetadata=IDENT_CURRENT('metadata')

				--Copiar la ruta actual, dado que no se agrega un nuevo archivo en la versión

				set @RutaAlmacenamiento=(select ruta from almacenamiento 
				where id_archivo=@IdArchivo and version_archivo=@VersionArchivo)

				insert into almacenamiento(ruta,id_archivo,version_archivo,id_metadata,id_extension,id_revision)
				values(@RutaAlmacenamiento,@IdArchivo,@VersionArchivo+1,@IdMetadata,@IdExtension,1)

				select 1 as 'INFO'
			end
			else
			begin
				set @IdMetadata=(select id_metadata from almacenamiento 
				where id_archivo=@IdArchivo and version_archivo=@VersionArchivo)
				--Actualizar los metadatos existentes.
				update metadata
				set descriptores=@DescriptorEs,
				descriptoren=@DescriptorEn,
				oemsku=@Oemsku,
				descriptorextra=@DescriptorExtra
				where id_metadata=@IdMetadata

				select 2 as 'INFO'
			end
		
		commit transaction T1
		
		end try
		begin catch
			select 0 as 'INFO'
			rollback transaction T1
		end catch
end

