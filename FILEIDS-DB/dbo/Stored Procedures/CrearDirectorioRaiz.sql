--Creación de directorio raiz o proyecto
CREATE PROCEDURE [dbo].[CrearDirectorioRaiz]
	@NombreDirectorioRaiz varchar(50),
	@DescripcionDirectorioRaiz varchar(500)
AS
	--Verificar si existe el nombre del directorio
	if exists(select * from directorios where nombre_directorio like @NombreDirectorioRaiz)
	begin

		update directorios
		set nombre_directorio=@NombreDirectorioRaiz,
		descriptor_directorio=@DescripcionDirectorioRaiz,
		activo=1
		where nombre_directorio like @NombreDirectorioRaiz 
		select 0
	end
	else
	begin
		insert into directorios (nombre_directorio,descriptor_directorio,activo,id_raiz,id_padre) values (@NombreDirectorioRaiz,@DescripcionDirectorioRaiz,1,null,null)
		select 1
	end

RETURN 0
