﻿--create procedure getFileProjectAssociationFromId(@id INT)
--as
--begin
--	select
--		proyectos.descriptores as "Descripcion Proyecto",
--		proyectos.descriptoren as "Project Description",
--		tipos_entregables.abreviatura as "Tipo Entregable",
--		tipos_entregables.id as "id Tipo Entregable",
--		proyectos.id as "id Proyecto"
--	from entregables, proyectos, tipos_entregables
--	where entregables.id_archivo=@id and entregables.id_proyecto=proyectos.id and tipos_entregables.id=entregables.id_tipo
--end;