namespace G2_SC603_KN_Proyecto.Models.ViewModels.Wod
{
    /// <summary>
    /// Representa un ejercicio dentro del detalle de un entrenamiento (WOD).
    /// </summary>
    public class WodEjercicioDetalleViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string? GrupoMuscular { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public int? Descanso { get; set; }
    }
}
