﻿--CREATE PROCEDURE getNombreEntregable
--	(@id INT)
--AS
--BEGIN
--	SELECT proyectos.codigo + ' - ' + tipos_entregables.abreviatura + ' - ' + format(archivos.id,'000-000')
--	FROM archivos, extensiones, tipos_entregables, proyectos, entregables
--	WHERE archivos.id_extension=extensiones.id AND entregables.id_proyecto=proyectos.id AND entregables.id_archivo=archivos.id and archivos.id=@id and entregables.id_tipo=tipos_entregables.id
--END;
