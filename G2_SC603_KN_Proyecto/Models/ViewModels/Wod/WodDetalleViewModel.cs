namespace G2_SC603_KN_Proyecto.Models.ViewModels.Wod
{
    /// <summary>
    /// Detalle completo de un entrenamiento (WOD). Se usa en la vista de
    /// Detalle accesible tanto desde el Historial (RMGM-WOD-003) como desde
    /// el Entrenamiento Diario (RMGM-WOD-002), cumpliendo el Escenario 3 de
    /// ambas historias.
    /// </summary>
    public class WodDetalleViewModel
    {
        public int IdRutina { get; set; }
        public int IdEntrenador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Objetivo { get; set; }
        public string NombreEntrenador { get; set; } = string.Empty;

        public List<WodEjercicioDetalleViewModel> Ejercicios { get; set; } = new();

        /// <summary>
        /// Clientes que tienen asignado este entrenamiento. Solo es
        /// relevante para Entrenador/Administrador; para un Cliente
        /// consultando su propio historial no aporta información nueva,
        /// pero no representa un riesgo mostrarla ya que solo llega a esta
        /// vista si el servicio ya validó su acceso al registro.
        /// </summary>
        public List<WodClienteAsignadoViewModel> ClientesAsignados { get; set; } = new();
    }
}
