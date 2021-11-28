CREATE PROCEDURE [dbo].[ListarNumerosVersiones]
	@IdArchivo int

AS
begin
	if exists(select id_archivo from archivos where id_archivo=@IdArchivo)
	begin
		select version_archivo as 'version'
		from metadata,almacenamiento,archivos where 
		almacenamiento.id_metadata=metadata.id_metadata and 
		almacenamiento.id_archivo=@IdArchivo and 
		archivos.id_archivo=almacenamiento.id_archivo and
		activo=1
	end	
end
