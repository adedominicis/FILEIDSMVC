--Retorna todos los hijos de un directorio. Se le suministra el ID del directorio

CREATE PROCEDURE [dbo].[DesarrollarDirectorioRecursivo]
    @IdDirectorio int
AS
    with desarrollo as (
    select id_directorio,
    nombre_directorio,
    id_padre,0 AS profundidad
    from directorios
    where id_directorio = @IdDirectorio and activo=1
    union all
    select hijo.id_directorio,
    hijo.nombre_directorio,
    hijo.id_padre,
    profundidad + 1
    from directorios as hijo
    join desarrollo as padre on padre.id_directorio = hijo.id_padre 
    where activo=1

    )
    select * from desarrollo
RETURN 0
