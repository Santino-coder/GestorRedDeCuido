$(document).ready(function () {
    // Función que se ejecuta cuando se escribe en el campo de búsqueda con clase "search"
    $(".search").keyup(function () {
        var searchTerm = $(".search").val(); // Obtiene el término de búsqueda
        var listItem = $('.results tbody').children('tr');
        var searchSplit = searchTerm.replace(/ /g, "'):containsi('");

        // Extiende jQuery para agregar un selector personalizado ":containsi"
        $.extend($.expr[':'], {
            'containsi': function (elem, i, match, array) {
                return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
            }
        });

        // Oculta las filas que no coinciden con la búsqueda y muestra las que coinciden
        $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'false');
        });

        $(".results tbody tr:containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'true');
        });

        // Cuenta y muestra la cantidad de elementos que coinciden con la búsqueda
        var jobCount = $('.results tbody tr[visible="true"]').length;
        $('.counter').text(jobCount + ' item');

        // Muestra un mensaje si no se encuentra ningún resultado
        if (jobCount == '0') { $('.no-result').show(); }
        else { $('.no-result').hide(); }

        // Llama a la función para ordenar la tabla
        ordenarTabla();
    });
});

// Función para ordenar la tabla alfabéticamente por la columna "Nombre"
function ordenarTabla() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("miTabla");
    switching = true;

    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("tr");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("td")[1]; // Cambia el índice para ordenar por otra columna
            y = rows[i + 1].getElementsByTagName("td")[1]; // Cambia el índice para ordenar por otra columna
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

// Llama a la función de ordenar al cargar la página
window.onload = ordenarTabla;

// Función para descargar una factura o proforma
function descargarDocumento(dataUrl, nombreDocumento) {
    var enlaceDescarga = document.createElement('a');
    enlaceDescarga.href = dataUrl;
    enlaceDescarga.download = nombreDocumento + '.jpg'; // Nombre del documento para descargar
    document.body.appendChild(enlaceDescarga);
    enlaceDescarga.click();
    document.body.removeChild(enlaceDescarga);
}
