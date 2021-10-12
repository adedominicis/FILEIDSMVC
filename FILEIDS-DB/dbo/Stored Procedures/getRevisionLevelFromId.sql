create procedure getRevisionLevelFromId(@id int)
as
begin
	select caracter
	from revisionlevels
	where id=@id;
end
