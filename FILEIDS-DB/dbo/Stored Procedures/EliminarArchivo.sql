CREATE PROCEDURE [dbo].[EliminarArchivo]
	@IdArchivo int
as
begin
	if exists(select id_archivo from archivos where id_archivo=@IdArchivo)
	begin
		update archivos
		set activo=0
		where id_archivo=@IdArchivo
		select 1 as 'INFO'
	end
	else
	begin
		select 0 as 'INFO'
	end
end

