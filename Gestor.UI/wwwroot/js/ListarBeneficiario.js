$(document).ready(function () {
    $(".search").keyup(function () {
        var searchTerm = $(".search").val();
        var listItem = $('.results tbody').children('tr');
        var searchSplit = searchTerm.replace(/ /g, "'):containsi('")

        $.extend($.expr[':'], {
            'containsi': function (elem, i, match, array) {
                return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
            }
        });

        $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'false');
        });

        $(".results tbody tr:containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'true');
        });

        var jobCount = $('.results tbody tr[visible="true"]').length;
        $('.counter').text(jobCount + ' item');

        if (jobCount == '0') { $('.no-result').show(); }
        else { $('.no-result').hide(); }
    });
});



document.getElementById('miBotonOrdenar').addEventListener('click', function () {
    var table = document.getElementById('miTabla');
    var rows = Array.from(table.querySelectorAll('tr')); // Obtienen todas las filas de la tabla
    var header = rows.shift(); // Elimina el encabezado de la tabla


    rows.sort(function (a, b) {
        var nameA = a.querySelector('td:nth-child(2)').textContent.trim().toLowerCase(); // Asume que la columna del nombre es la segunda 
        var nameB = b.querySelector('td:nth-child(2)').textContent.trim().toLowerCase();

        return nameA.localeCompare(nameB); // Compara los nombres de forma insensible a mayúsculas y minúsculas
    });

    // Vuelve a agregar las filas ordenadas a la tabla
    rows.forEach(function (row) {
        table.appendChild(row);
    });

    // Vuelve a agregar el encabezado
    table.insertBefore(header, table.firstChild);
});
