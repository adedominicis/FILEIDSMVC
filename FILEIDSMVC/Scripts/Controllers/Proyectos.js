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
}

//Mostrar el modal donde se edita la metadata.
function MostrarModalMetadata(IdArchivo, NombreArchivo, Extension) {

    LimpiarModalMetadata();
    $('#mdMetadata').modal('show')
    //Llenar modal con la data básica.
    $('#lblNombreArchivo').html(NombreArchivo + '.' + Extension)
    $("#IdArchivo").val(IdArchivo)
    $("#NombreArchivo").val(NombreArchivo)
    $("#Extension").val(Extension)

}


//Entrega una lista simple con los numeros de version existentes de un archivo
function ListarVersionesArchivo(IdArchivo) {
    console.log('Obteniendo versiones de archivo: '+IdArchivo)
    $.ajax({
        type: "POST",
        url: "/Metadata/GetNumerosVersiones",
        data: { IdArchivo: IdArchivo},
        async: false,
        success: function (res) {
            var versiones = JSON.parse(res);
            //Ciclo para llenar el combobox aqui. Es bueno hacer una función para eso, se usa bastante.
        }
    });

}

//Solicitar metadata para llenar modal.
function ObtenerMetadata(IdArchivo, NombreArchivo, Extension, Version) {
    console.log('Solicitando metadata de :' +IdArchivo)
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
        }
    });

}
function GuardarMetadataAsync() {


}

//Convierte un json en elementos para un select.
function SelectHtmlFromJson(listaJson) {

    var $select = $('#datas');
    $.each(data, function (i, val) {
        $select.append($('<option />', { value: (i + 1), text: val[i + 1] }));
    });
}
