// Obtén referencias a los elementos del DOM
const hamburger = document.querySelector(".hamburger"); // Botón del menú desplegable
const navMenu = document.querySelector(".nav-menu"); // Menú de navegación

// Agrega un evento de clic al botón del menú desplegable
hamburger.addEventListener("click", () => {
    // Alterna la clase 'active' en el botón del menú desplegable y el menú de navegación para mostrar/ocultar el menú
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
});

// Agrega un evento de clic a todos los elementos con la clase 'nav-link' en el menú de navegación
document.querySelectorAll(".nav-link").forEach(navLink => {
    navLink.addEventListener("click", () => {
        // Al hacer clic en un enlace del menú, se asegura de que el menú se oculte (eliminando la clase 'active' del botón del menú y el menú de navegación)
        hamburger.classList.remove("active");
        navMenu.classList.remove("active");
    });
});
