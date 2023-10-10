function generatePDF() {
    var doc = new jsPDF();
    doc.fromHTML(document.getElementById("html2pdf"), 10, 10, {
        'width': 100
    },
        function (a) {
            doc.save("DetallesDeBeneficiario.pdf");
        });
}