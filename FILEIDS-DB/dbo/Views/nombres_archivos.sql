﻿--create view nombres_archivos
--as
--	select format([metadata].id,'000-000') + ' - ' + descriptores as 'NOMBRE ARCHIVO', extensiones.nombre as 'TIPO'
--	from [metadata], extensiones
--	where [metadata].id_extension=extensiones.id;
