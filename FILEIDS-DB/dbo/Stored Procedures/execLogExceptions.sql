create procedure execLogExceptions(@mensaje varchar(300))
as
begin
	insert into excepciones
	values(@mensaje, GETDATE())
end
