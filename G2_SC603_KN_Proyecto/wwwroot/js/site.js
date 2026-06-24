tailwind.config = {
    darkMode: "class",
    theme: {
        extend: {
            colors: {
                "surface-container-low": "#1c1b1b",
                "tertiary-fixed": "#ffd9dc",
                "primary-fixed-dim": "#cfbcff",
                "on-surface-variant": "#cbc3d8",
                "on-secondary-fixed": "#290055",
                "primary-fixed": "#e9ddff",
                "tertiary": "#ffb2ba",
                "on-primary-fixed": "#22005d",
                "tertiary-fixed-dim": "#ffb2ba",
                "secondary-fixed-dim": "#d8b9ff",
                "surface": "#131313",
                "surface-container-lowest": "#0e0e0e",
                "on-secondary-container": "#cda8ff",
                "secondary-fixed": "#eddcff",
                "surface-bright": "#3a3939",
                "inverse-primary": "#6d30e9",
                "tertiary-container": "#96545d",
                "secondary": "#d8b9ff",
                "inverse-on-surface": "#313030",
                "on-error-container": "#ffdad6",
                "on-secondary": "#450087",
                "surface-container-high": "#2a2a2a",
                "on-tertiary-fixed": "#390a14",
                "on-secondary-fixed-variant": "#5c279f",
                "primary": "#cfbcff",
                "surface-variant": "#353534",
                "surface-container-highest": "#353534",
                "outline": "#958da1",
                "inverse-surface": "#e5e2e1",
                "secondary-container": "#5f2aa2",
                "outline-variant": "#494455",
                "on-tertiary-fixed-variant": "#6f353d",
                "surface-tint": "#cfbcff",
                "primary-container": "#763df2",
                "error-container": "#93000a",
                "on-primary-container": "#ede2ff",
                "on-tertiary-container": "#ffdfe1",
                "on-background": "#e5e2e1",
                "error": "#ffb4ab",
                "on-primary": "#3a0092",
                "surface-dim": "#131313",
                "on-primary-fixed-variant": "#5400cc",
                "surface-container": "#201f1f",
                "on-tertiary": "#541f28",
                "background": "#131313",
                "on-surface": "#e5e2e1",
                "on-error": "#690005"
            },
            borderRadius: {
                "DEFAULT": "0.125rem",
                "lg": "0.25rem",
                "xl": "0.5rem",
                "2xl": "1rem",
                "full": "9999px"
            },
            fontFamily: {
                "headline": ["Manrope", "sans-serif"],
                "body": ["Inter", "sans-serif"],
                "label": ["Inter", "sans-serif"]
            }
        }
    }
}

// VISTA: LOGIN
// El togglePassword solo se enlaza si el elemento existe en la pagina actual
document.addEventListener("DOMContentLoaded", function () {
    const togglePassword = document.getElementById("togglePassword");
    if (togglePassword) {
        togglePassword.addEventListener("click", function () {
            const passInput = document.getElementById("loginPass");
            const icon = document.getElementById("toggleIcon");
            if (passInput.type === "password") {
                passInput.type = "text";
                icon.textContent = "visibility";
            } else {
                passInput.type = "password";
                icon.textContent = "visibility_off";
            }
        });
    }
});

// ===================== MODALES GENERICOS =====================
function closeModal(modalId) {
    const modal = document.getElementById(modalId);
    if (!modal) return;
    modal.style.display = "none";
    modal.classList.remove("active");
    modal.querySelectorAll("input").forEach(function (input) { input.value = ""; });
}
function openModal(modalId) {
    const modal = document.getElementById(modalId);
    if (!modal) return;
    modal.classList.add("active");
    modal.style.display = "flex";
}
function openClientModal() {
    const modal = document.getElementById("clientModal");
    if (!modal) return;
    modal.classList.add("active");
    modal.style.display = "flex";
}

function showTab(tabId) {
    document.querySelectorAll('.tab-content').forEach(el => el.classList.add('hidden'));
    document.querySelectorAll('.tab-btn').forEach(el => el.classList.remove('active'));
    document.getElementById(tabId).classList.remove('hidden');
    event.target.classList.add('active');
}

// ===================== VISTA: MOSTRAR CLIENTES =====================
function filterClients() {
    const searchValue = document.getElementById("clientSearch").value.toLowerCase();
    const statusValue = document.getElementById("clientStatusFilter").value;
    const rows = document.querySelectorAll("#clientsTableBody tr");

    let visibleCount = 0;

    rows.forEach(row => {
        const name = row.cells[0] ? row.cells[0].textContent.toLowerCase() : "";
        const cel = row.cells[1] ? row.cells[1].textContent.toLowerCase() : "";
        const status = row.cells[5] ? row.cells[5].textContent : "";

        const matchesSearch = name.includes(searchValue);
        const matchesSearchCel = cel.includes(searchValue);
        const matchesStatus = statusValue === "" || status === statusValue;

        if ((matchesSearch || matchesSearchCel) && matchesStatus) {
            row.style.display = "";
            visibleCount++;
        } else {
            row.style.display = "none";
        }
    });

    const counter = document.getElementById("clientsCount");
    if (counter) counter.textContent = `Mostrando ${visibleCount} cliente(s)`;
}

function openDetailsModal(element) {
    const modal = document.getElementById("detailsModal");
    if (!modal) return;
    document.getElementById("detalleNombre").value = element.dataset.nombre || "";
    document.getElementById("detalleCedula").value = element.dataset.cedula || "";
    document.getElementById("detalleTelefono").value = element.dataset.telefono || "";
    document.getElementById("detalleCorreo").value = element.dataset.correo || "";
    document.getElementById("detalleFecha").value = element.dataset.fecha || "";
    document.getElementById("detalleEstado").value = element.dataset.estado || "";
    document.getElementById("detalleMembresia").value = element.dataset.membresia || "";
    document.getElementById("detalleVencimiento").value = element.dataset.vencimiento || "";
    modal.classList.add("active");
    modal.style.display = "flex";
}

function openEditModal(btn) {
    const modal = document.getElementById("editModal");
    if (!modal) return;
    document.getElementById("editIdCliente").value = btn.dataset.id || "";
    document.getElementById("editNombre").value = btn.dataset.nombre || "";
    document.getElementById("editCedula").value = btn.dataset.cedula || "";
    document.getElementById("editTelefono").value = btn.dataset.telefono || "";
    document.getElementById("editCorreo").value = btn.dataset.correo || "";
    document.getElementById("editFecha").value = btn.dataset.fecha || "";
    const selectEstado = document.getElementById("editEstado");
    if (selectEstado) selectEstado.value = btn.dataset.estado || "Activo";
    modal.classList.add("active");
    modal.style.display = "flex";
}

function confirmarEliminacion(idCliente, nombreCliente) {
    const modal = document.getElementById("deleteModal");
    if (!modal) return;
    document.getElementById("deleteClienteNombre").textContent = nombreCliente;
    document.getElementById("btnConfirmarEliminar").onclick = function () {
        document.getElementById("formEliminar_" + idCliente).submit();
    };
    modal.classList.add("active");
    modal.style.display = "flex";
}
// ===================== VISTA: MOSTRAR MEMBRESIA =====================
function filterMembresia() {
    const searchValue = document.getElementById("membresiaSearch").value.toLowerCase();
    const statusValue = document.getElementById("membresiaStatusFilter").value;
    const rows = document.querySelectorAll("#membershipsTableBody tr");

    let visibleCount = 0;

    rows.forEach(row => {
        const name = row.cells[0] ? row.cells[0].textContent.toLowerCase() : "";
        const cel = row.cells[1] ? row.cells[1].textContent.toLowerCase() : "";
        const status = row.cells[5] ? row.cells[5].textContent : "";

        const matchesSearch = name.includes(searchValue);
        const matchesSearchCel = cel.includes(searchValue);
        const matchesStatus = statusValue === "" || status === statusValue;

        if ((matchesSearch || matchesSearchCel) && matchesStatus) {
            row.style.display = "";
            visibleCount++;
        } else {
            row.style.display = "none";
        }
    });

    const counter = document.getElementById("clientsCount");
    if (counter) counter.textContent = `Mostrando ${visibleCount} cliente(s)`;
}

function openEditModalMembresia(btn) {
    const modal = document.getElementById("openEditModalMembresia");
    if (!modal) return;
    document.getElementById("editIdCliente").value = btn.dataset.idcliente || "";
    document.getElementById("editNombre").value = btn.dataset.nombre || "";
    document.getElementById("editMembershipStart").value = btn.dataset.fechainicio || "";
    document.getElementById("editMembershipEnd").value = btn.dataset.fechafin || "";
    const selectEstado = document.getElementById("editMembershipStatus");
    if (selectEstado) selectEstado.value = btn.dataset.estado || "Activa";
    const selectMembresia = document.getElementById("editMembershipPlan");
    if (selectMembresia) selectMembresia.value = btn.dataset.idmembresia || "";
    modal.classList.add("active");
    modal.style.display = "flex";
}

// ===================== VISTA: MOSTRAR USUARIOS =====================
function filterUsers() {
    const searchValue = document.getElementById("clientSearch").value.toLowerCase();
    const statusValue = document.getElementById("clientStatusFilter").value;
    const rows = document.querySelectorAll("#clientsTableBody tr");

    let visibleCount = 0;

    rows.forEach(row => {
        const name = row.cells[2] ? row.cells[2].textContent.toLowerCase() : "";
        const status = row.cells[1] ? row.cells[1].textContent : "";

        const matchesSearch = name.includes(searchValue);
        const matchesStatus = statusValue === "" || status === statusValue;

        if (matchesSearch && matchesStatus) {
            row.style.display = "";
            visibleCount++;
        } else {
            row.style.display = "none";
        }
    });

    const counter = document.getElementById("clientsCount");
    if (counter) counter.textContent = `Mostrando ${visibleCount} usuario(s)`;
}

function openEditUserModal(username, nombre, correo, rol, telefono) {
    const modal = document.getElementById("editUserModal");
    if (!modal) return;
    document.getElementById("editUserId").value = username;
    document.getElementById("editUsername").value = username;
    document.getElementById("editNombre").value = nombre;
    document.getElementById("editCorreo").value = correo;
    document.getElementById("editRol").value = rol;
    document.getElementById("editTelefono").value = telefono || "";
    modal.classList.add("active");
    modal.style.display = "flex";
}

// ===================== VISTA: MOSTRAR WOD =====================
function openWODModal() {
    const modal = document.getElementById("wodModal");
    if (!modal) return;
    const exerciseList = document.getElementById("exerciseList");
    if (exerciseList) exerciseList.innerHTML = "";
    const wodNombre = document.getElementById("wodNombre");
    if (wodNombre) wodNombre.value = "";
    const wodObjetivo = document.getElementById("wodObjetivo");
    if (wodObjetivo) wodObjetivo.value = "";
    const errorEj = document.getElementById("errorEjercicios");
    if (errorEj) errorEj.classList.add("hidden");
    if (typeof agregarFilaEjercicio === "function") agregarFilaEjercicio();
    modal.classList.add("active");
    modal.style.display = "flex";
}

function openEditEquipoModal(idEquipo, nombre, estado, fechaCompra, costo) {

    const modal = document.getElementById("editEquipoModal");

    if (!modal) return;

    document.getElementById("editIdEquipo").value = idEquipo;
    document.getElementById("editNombre").value = nombre;
    document.getElementById("editEstado").value = estado;
    document.getElementById("editFechaCompra").value = fechaCompra || "";
    document.getElementById("editCosto").value = costo || "";

    modal.classList.add("active");
    modal.style.display = "flex";
}

function openAddEquipoModal() {
    document.getElementById("addEquipoModal").classList.add("active");
}

// ===================== TOAST GENERICO =====================
// Reutilizable en todo el proyecto. type: "success" | "error" | "warning"
let toastTimeoutId = null;

function showToast(message, type) {
    const toast = document.getElementById("toast");
    const icon = document.getElementById("toastIcon");
    const text = document.getElementById("toastMessage");

    if (!toast || !icon || !text) return;

    const config = {
        success: { iconName: "check_circle", color: "#22c55e" },
        error: { iconName: "error", color: "#ef4444" },
        warning: { iconName: "warning", color: "#f59e0b" }
    };

    const selected = config[type] || config.success;

    icon.textContent = selected.iconName;
    icon.style.color = selected.color;
    text.textContent = message;

    toast.classList.add("show");

    if (toastTimeoutId) {
        clearTimeout(toastTimeoutId);
    }

    toastTimeoutId = setTimeout(function () {
        toast.classList.remove("show");
    }, 4000);
}

document.addEventListener("DOMContentLoaded", function () {
    if (window.__toastData && window.__toastData.message) {
        showToast(window.__toastData.message, window.__toastData.type);
    }
});