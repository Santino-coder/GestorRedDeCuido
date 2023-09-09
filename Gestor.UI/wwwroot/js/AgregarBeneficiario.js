$(document).ready(function () {
    $(".column-to-hide").each(function () {
        if ($(this).text().trim() === "") {
            $(this).closest("tr").find(".column-to-hide").addClass("hide-column");
        }
    });
});