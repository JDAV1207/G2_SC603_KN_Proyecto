
function cambiarPeriodo(periodo) {

    document.querySelectorAll(".tab-btn")
        .forEach(x => x.classList.remove("active"));

    if (periodo === "semana") {
        document.getElementById("btnSemana").classList.add("active");
        cargarGrafico(asistenciaSemana);
    }
    else {
        document.getElementById("btnMes").classList.add("active");
        cargarGrafico(asistenciaMes);
    }

}

let attendanceChart;

function cargarGrafico(datos) {

    const labels = datos.map(x => x.Dia);
    const valores = datos.map(x => x.Cantidad);

    if (attendanceChart) {
        attendanceChart.destroy();
    }

    attendanceChart = new Chart(
        document.getElementById("attendanceChart"),
        {
            type: "bar",

            data: {
                labels: labels,
                datasets: [{
                    label: "Asistencias",
                    data: valores,
                    borderWidth: 1
                }]
            },

            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });
}


document.addEventListener("DOMContentLoaded", function () {

    cambiarPeriodo("semana");

});