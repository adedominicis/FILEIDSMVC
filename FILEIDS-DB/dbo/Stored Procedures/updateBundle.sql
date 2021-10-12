--create procedure updateBundle(
--	@id int,
--	@descriptores varchar(255),
--	@descriptoren varchar(255),
--	@oemsku varchar(255),
--	@descriptorextra varchar(255),
--	@idextension int,
--	@idtipoentregable int,
--	@idproyecto int)
--as
--begin

--	UPDATE archivos SET 
--archivos.descriptores = @descriptores,
--archivos.descriptoren = @descriptoren,
--archivos.oemsku = @oemsku,
--archivos.id_extension=@idextension,
--archivos.descriptorextra=@descriptorextra
--WHERE archivos.id=@id

--	/*  Revisar si existe una definicion de tipo de entregable y de proyecto primero*/

--	if (select count(*)
--	from entregables, proyectos, tipos_entregables
--	where entregables.id_archivo=@id and entregables.id_proyecto=proyectos.id and tipos_entregables.id=entregables.id_tipo)>0
--	begin
--		update entregables set
--		entregables.id_proyecto=@idproyecto,
--		entregables.id_tipo=@idtipoentregable
--		where entregables.id_archivo=@id
--	end
--else
--	begin
--		insert into entregables
--		values
--			(@id, @idproyecto, @idtipoentregable)
--	end


--end
