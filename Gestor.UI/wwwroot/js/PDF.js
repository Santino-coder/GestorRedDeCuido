// Función para generar un archivo PDF a partir de contenido HTML
function generatePDF() {
    var doc = new jsPDF({
        orientation: "portrait",
        unit: "mm",
        format: "a4",
        lineHeight: 1.5
    });

    // Convierte el contenido HTML del elemento con el id "html2pdf" en un archivo PDF
    doc.fromHTML(document.getElementById("html2pdf"), 10, 10, {
        'width': 190, // Ancho del área del contenido
        'whitePage': true // Conservar saltos de línea
    }, function (output) {
        // Guarda el PDF generado con el nombre "DetallesDeBeneficiario.pdf"
        doc.save("DetallesDeBeneficiario.pdf");
    });
}
