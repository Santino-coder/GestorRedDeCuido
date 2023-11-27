$(document).ready(function () {
    // Cuando se hace clic en el botón con el id "mostrarAlternativa2"
    $("#mostrarAlternativa2").click(function () {
        // Mostrar el elemento con el id "Alternativa2" y ocultar el botón "mostrarAlternativa2"
        $("#Alternativa2").show();
        $(this).hide();
    });

    // Repite el mismo patrón para otros botones y elementos div
    $("#mostrarAlternativa3").click(function () {
        $("#Alternativa3").show();
        $(this).hide();
    });

    $("#mostrarAlternativa4").click(function () {
        $("#Alternativa4").show();
        $(this).hide();
    });

    $("#mostrarAlternativa5").click(function () {
        $("#Alternativa5").show();
        $(this).hide();
    });
});
