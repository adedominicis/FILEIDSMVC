﻿--CREATE PROCEDURE getNombreArchivo
--	(@id INT)
--AS
--BEGIN
--	SELECT format([metadata].id,'000-000') + ' - ' + [metadata].descriptores + extensiones.extension as 'NOMBRE ARCHIVO'
--	FROM [metadata], extensiones
--	WHERE [metadata].id_extension=extensiones.id AND [metadata].id=@id
--END;
