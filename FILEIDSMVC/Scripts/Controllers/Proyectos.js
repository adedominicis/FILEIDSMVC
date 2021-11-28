$(document).ready(function () {

    //Versión fue seleccionada.
    $("#cbSeleccionVersion").change(function () {

        var Version = $("#cbSeleccionVersion option:selected").val()
        ObtenerMetadata(
            $("#IdArchivo").val(),
            $("#NombreArchivo").val(),
            $("#Extension").val(),
            Version
        )
    });
});

//Limpiar todos los campos editables del modal de metadata.
function LimpiarModalMetadata() {

    $("#txDescriptorEs").val('')
    $("#txDescriptorEn").val('')
    $("#txDescriptorExtra").val('')
    $("#txOemSku").val('')
    $('#cbSeleccionVersion').val(0)

}

//Mostrar el modal donde se edita la metadata.
function MostrarModalMetadata(IdArchivo, NombreArchivo, Extension) {

    $('#mdMetadata').modal('show')
    //Llenar modal con la data básica.
    $('#lblNombreArchivo').html(NombreArchivo + '.' + Extension)
    $("#IdArchivo").val(IdArchivo)
    $("#NombreArchivo").val(NombreArchivo)
    $("#Extension").val(Extension)
    ListarVersionesArchivo(IdArchivo)

}


//Entrega una lista simple con los numeros de version existentes de un archivo
function ListarVersionesArchivo(IdArchivo) {
    console.log('Obteniendo versiones de archivo: ' + IdArchivo)
    $.ajax({
        type: "POST",
        url: "/Metadata/ListarNumerosVersiones",
        data: { IdArchivo: IdArchivo },
        async: false,
        success: function (res) {
            var versiones = JSON.parse(res);
            if (res == "Excepcion") {
                swal("Holycrap!")
            } else {
                //Ciclo para llenar el combobox aqui. Es bueno hacer una función para eso, se usa bastante.
                $('#cbSeleccionVersion').empty().append($('<option />', { value: 0, text: 'Seleccione una version' }))
                var $select = $('#cbSeleccionVersion');
                $.each(versiones, function (i) {
                    $select.append($('<option />', { value: (i + 1), text: (i + 1) }));
                });
            }

        }
    });

}

//Solicitar metadata para llenar modal.
function ObtenerMetadata(IdArchivo, NombreArchivo, Extension, Version) {
    console.log('Solicitando metadata de :' + IdArchivo)
    $.ajax({
        type: "POST",
        url: "/Metadata/GetMetadata",
        data: { IdArchivo: IdArchivo, VersionArchivo: Version },
        async: false,
        success: function (res) {
            var metadata = JSON.parse(res);
            LimpiarModalMetadata()
            //Actualizar modal.
            $("#txDescriptorEs").val(metadata[0].DescriptorEs)
            $("#txDescriptorEn").val(metadata[0].DescriptorEn)
            $("#txDescriptorExtra").val(metadata[0].DescriptorExtra)
            $("#txOemSku").val(metadata[0].Oemsku)
            //llenar combobox de versiones
            ListarVersionesArchivo(IdArchivo)
        }
    });

}
function ActualizarMetadata() {

    console.log('Guardando metadatos y creando nueva versión de:' + IdArchivo);

    $.ajax({
        type: "POST",
        url: "/Metadata/ActualizarMetadata",
        data: {
            IdArchivo: $("#IdArchivo").val(),
            DescriptorEs: $("#txDescriptorEs").val(),
            DescriptorEn: $("#txDescriptorEn").val(),
            DescriptorExtra: $("#txDescriptorExtra").val(),
            OemSku: $("#txOemSku").val()
        },
        async: false,
        success: function (res) {
            if (res == 0) {
                //Mensaje de error de alguna manera aqui
            } else {
                $('#mdMetadata').modal('hide')
                swal("Actualizado")
            }
        }
    });

}

//Convierte un json en elementos para un select.
function SelectHtmlFromJson(listaJson) {
    https://stackoverflow.com/questions/9071525/how-to-create-html-select-option-from-json-hash
    var $select = $('#datas');
    $.each(data, function (i, val) {
        $select.append($('<option />', { value: (i + 1), text: val[i + 1] }));
    });
}
