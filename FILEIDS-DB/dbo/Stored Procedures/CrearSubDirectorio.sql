CREATE PROCEDURE [dbo].[CrearSubDirectorio]

	@IdDirectorioPadre int,
	@IdDirectorioRaiz int,
	@NombreSubDirectorio varchar(50),
	@DescripciónSubDirectorio varchar(500)

AS
	--Si el directorio ya existe no se hace nada. Si no existe, se crea.
	if not exists(select * from directorios where nombre_directorio like @NombreSubDirectorio and id_padre=@IdDirectorioPadre and id_raiz=@IdDirectorioRaiz)
	begin
		insert into directorios (nombre_directorio,descriptor_directorio,activo,id_padre, id_raiz) values (@NombreSubDirectorio,@DescripciónSubDirectorio,1,@IdDirectorioPadre,@IdDirectorioRaiz)
	end

RETURN 0
