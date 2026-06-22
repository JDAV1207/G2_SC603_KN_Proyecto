namespace G2_SC603_KN_Proyecto.Models.ViewModels.Wod
{
    /// <summary>
    /// Representa la asignación de un entrenamiento a un cliente.
    /// Se usa en la vista de detalle para que Entrenadores y Administradores
    /// puedan ver a quién(es) se les asignó un WOD y cuándo.
    /// </summary>
    public class WodClienteAsignadoViewModel
    {
        public string NombreCliente { get; set; } = string.Empty;
        public DateOnly FechaAsignacion { get; set; }
    }
}
