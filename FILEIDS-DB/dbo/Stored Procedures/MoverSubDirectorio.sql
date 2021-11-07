--Mover subdirectorio. Funciona cambiando el id del padre y de la raiz

CREATE PROCEDURE [dbo].[MoverSubDirectorio]
	@IdDirectorioMover int,
	@IdNuevoDirectorioRaiz int,
	@IdNuevoDirectorioPadre int
	
AS
	--Solo pueden moverse directorios activos.
	if exists(select id_directorio from directorios where id_directorio=@IdDirectorioMover and activo=1)
	begin
		update directorios
		set id_raiz=@IdNuevoDirectorioRaiz,id_padre=@IdNuevoDirectorioPadre
		where id_directorio=@IdDirectorioMover and activo=1
	end
return 0