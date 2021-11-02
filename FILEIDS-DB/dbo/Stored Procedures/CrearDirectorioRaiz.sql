--Creación de directorio raiz o proyecto
CREATE PROCEDURE [dbo].[CrearDirectorioRaiz]
	@Nombre varchar(50),
	@Descripcion varchar(500)
AS
	--Verificar si existe el nombre del directorio
	if exists(select * from directorios where nombre_directorio like @Nombre)
	begin

		update directorios
		set nombre_directorio=@Nombre,
		descriptor_directorio=@Descripcion,
		activo=1
		where nombre_directorio like @Nombre
		select 0
	end
	else
	begin
		insert into directorios (nombre_directorio,descriptor_directorio,activo) values (@Nombre,@Descripcion,1)
		select 1
	end

RETURN 0
