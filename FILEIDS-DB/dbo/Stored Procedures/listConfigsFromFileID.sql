﻿--create procedure listConfigsFromFileID(@partid int)
--as
--BEGIN
--    select RIGHT(archivos.fullpath,CHARINDEX('\',REVERSE(archivos.fullpath))-1) as 'Nombre de Archivo', configuraciones.nombre as 'Configuración'
--    from archivos, configuraciones
--    where archivos.id=@partid
--END
