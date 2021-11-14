CREATE PROCEDURE [dbo].[MoverArchivo]
	@IdArchivo int,
	@IdNuevoDirectorio int
	
AS
begin
	if exists(select id_directorio from directorios where id_directorio=@IdNuevoDirectorio)
	begin
		update archivos
		set id_carpeta_padre=@IdNuevoDirectorio
		where id_archivo=@IdArchivo
		select 1 as 'INFO'
	end
	else
	begin
		select 0 as 'INFO'
	end
end
