CREATE PROCEDURE [dbo].[VerificarMD5]
	@md5 varchar(32)
AS
begin
	select id_almacenamiento from almacenamiento where md5=@md5
end
