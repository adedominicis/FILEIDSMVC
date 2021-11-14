CREATE PROCEDURE [dbo].[CrearArchivo]
	@NombreArchivo varchar(50),
	@IdCarpetaPadre int,
	@ExtensionArchivo varchar(10),
	@FileSystemRoot varchar(256)

AS
begin

declare @IdArchivo int
declare @IdExtension int
declare @IdMetadata int
declare @IdAlmacenamiento int
declare @RutaAlmacenamiento varchar(256)

begin transaction T1
	begin try
		--Transacción de inserción de archivo
		--1 Buscar id de extensión, si no existe, crearla.

		if not exists(select id_extension from extensiones where extension_extension=@ExtensionArchivo)
		begin
			insert into extensiones(extension_descripcion,extension_extension)
			values('custom',@ExtensionArchivo)
		end
		set @IdExtension=(select id_extension from extensiones where extension_extension=@ExtensionArchivo)

		--2 Creación de archivo nuevo
		insert into archivos(nombre_archivo,id_revisionlevel,id_carpeta_padre,activo)
		values(@NombreArchivo,1,@IdCarpetaPadre,1)

		-- 3 Obtener ID del archivo nuevo.
		set @IdArchivo=IDENT_CURRENT('archivos')

		-- 4 Crear slot de metadatos. Los metadatos pueden llenarse despues, es mas simple el workflow.
		insert into metadata(descriptores,descriptoren,oemsku,descriptorextra,partid) values (null,null,null,null,null);
		set @IdMetadata=IDENT_CURRENT('metadata')

		-- 5 Crear almacenamiento.
		set @IdAlmacenamiento=IDENT_CURRENT('almacenamiento')+1
		set @RutaAlmacenamiento=@FileSystemRoot+'\'+CONVERT(varchar(20), @IdAlmacenamiento)+'.'+@ExtensionArchivo

		--6 Verificar si existe un nivel mínimo de revision
		if not exists(select id_revisionlevel from revisionlevels where id_revisionlevel=1)
		begin
			insert into revisionlevels values('-')
		end


		insert into almacenamiento(ruta,id_archivo,version_archivo,id_metadata,id_extension,id_revision)
		values(@RutaAlmacenamiento,@IdArchivo,1,@IdMetadata,@IdExtension,1)

		select 
		@IdArchivo as 'ID_ARCHIVO',
		1 as 'VERSION_ARCHIVO',
		@IdAlmacenamiento as 'ID_ALMACENAMIENTO',
		@RutaAlmacenamiento as 'RUTA_ALMACENAMIENTO',
		@IdMetadata as 'ID_METADATA'

		commit transaction T1
	end try
	begin catch
		select ERROR_MESSAGE() as 'ERROR'
		rollback transaction T1
	end catch
	
end