// Función para generar un archivo PDF a partir de contenido HTML
function generatePDF() {
    var doc = new jsPDF(); // Crea una instancia de jsPDF para generar el PDF

    // Convierte el contenido HTML del elemento con el id "html2pdf" en un archivo PDF
    doc.fromHTML(document.getElementById("html2pdf"), 10, 10, {
        'width': 100 // Ancho del área del contenido
    }, function (output) {
        // Guarda el PDF generado con el nombre "DetallesDeBeneficiario.pdf"
        doc.save("DetallesDeBeneficiario.pdf");
    });
}
