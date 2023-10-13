function mostrarDiv(id) {
    var div = document.getElementById(id);
    if (div.style.display !== 'block') {
        ocultarTodosLosDivs();
        div.style.display = 'block';
    }
}
/*
function ocultarPrimerosDivs() {
    var divs = document.querySelectorAll('.contenedor-div');
    for (var i = 0; i < divs.length; i++) {
        if (i < 2) { // Solo ocultar los dos primeros divs
            divs[i].style.display = 'none';
        }
    }
}
*/

function ocultarTodosLosDivs() {
    var divs = document.querySelectorAll('.contenedor-div');
    for (var i = 0; i < divs.length; i++) {
        divs[i].style.display = 'none';
    }
}

function mostrarDivMes() {
    mostrarDiv('TotalPorMes');
}

function mostrarDivAnio() {
    mostrarDiv('TotalPorAnio');
}

function mostrarDivBeneficiario() {
    mostrarDiv('TotalPorBeneficiario');
}

function mostrarDivAlternativa() {
    mostrarDiv('TotalPorAlternativa');
}