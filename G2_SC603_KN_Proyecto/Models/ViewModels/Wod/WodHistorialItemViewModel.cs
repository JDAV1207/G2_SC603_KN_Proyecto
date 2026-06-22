namespace G2_SC603_KN_Proyecto.Models.ViewModels.Wod
{
    /// <summary>
    /// Representa un elemento de lista para el Historial de Entrenamientos
    /// (RMGM-WOD-003) y para el Entrenamiento Diario (RMGM-WOD-002).
    /// Ambas pantallas muestran el mismo tipo de información (un WOD con su
    /// fecha asociada); solo cambia el filtro de fecha aplicado en el
    /// servicio, por lo que comparten este mismo ViewModel para evitar
    /// duplicar clases equivalentes.
    /// </summary>
    public class WodHistorialItemViewModel
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Objetivo { get; set; }
        public string NombreEntrenador { get; set; } = string.Empty;
        public int CantidadEjercicios { get; set; }

        /// <summary>
        /// Fecha de asignación del entrenamiento (tabla cliente_rutina).
        /// Es el dato de fecha más cercano disponible en el modelo a una
        /// "fecha de publicación", ya que la tabla rutina no posee columna
        /// de fecha. Puede ser null cuando un Entrenador/Administrador
        /// consulta una rutina que aún no se le ha asignado a ningún cliente.
        /// </summary>
        public DateOnly? Fecha { get; set; }

        /// <summary>
        /// Solo se completa cuando quien consulta es Entrenador o
        /// Administrador, para identificar a qué cliente pertenece el
        /// registro en el Entrenamiento Diario.
        /// </summary>
        public string? NombreCliente { get; set; }
    }
}
