/*
Crea un archivo nuevo.
Antes de ejecutar este procdimiento ya se ha verificado desde el backend y con otros procedimientos que:
1- No existe un archivo de igual nombre y extension en la misma carpeta.
2- No existe el MD5 del archivo físico en todo el repositorio.
*/
CREATE PROCEDURE [dbo].[CrearArchivo]
	@NombreArchivo varchar(50),
	@IdCarpetaPadre int,
	@ExtensionArchivo varchar(10),
	@RutaAlmacenamiento varchar(256),
	@MD5 varchar(32)

AS
begin

declare @IdArchivo int
declare @IdExtension int
declare @IdMetadata int
declare @IdAlmacenamiento int

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

		--2 Verificar si existe un nivel mínimo de revision, si no existe, crearlo
		if not exists(select id_revisionlevel from revisionlevels where id_revisionlevel=1)
		begin
			insert into revisionlevels values('-')
		end

		--3 Crear nuevo archivo
		insert into archivos(nombre_archivo,id_revisionlevel,id_carpeta_padre,activo)
		values(@NombreArchivo,1,@IdCarpetaPadre,1)
		set @IdArchivo=IDENT_CURRENT('archivos')

		--4 Crear slot de metadatos. Los metadatos pueden llenarse despues, es mas simple el workflow.
		insert into metadata(descriptores,descriptoren,oemsku,descriptorextra,partid) values (null,null,null,null,null);
		set @IdMetadata=IDENT_CURRENT('metadata')

		--5 Crear slot de almacenamiento.
		insert into almacenamiento(ruta,id_archivo,version_archivo,id_metadata,id_extension,id_revision,MD5)
		values(@RutaAlmacenamiento,@IdArchivo,1,@IdMetadata,@IdExtension,1,@MD5)

		--6 Obtener el ID del almacenamiento
		select @IdAlmacenamiento=id_almacenamiento from archivos,almacenamiento,extensiones 
		where 
		nombre_archivo=@NombreArchivo and
		id_carpeta_padre=@IdCarpetaPadre and
		extensiones.id_extension=almacenamiento.id_extension and
		extensiones.extension_extension=@ExtensionArchivo and
		archivos.id_archivo=almacenamiento.id_archivo

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