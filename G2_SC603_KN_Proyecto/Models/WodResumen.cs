using System.ComponentModel.DataAnnotations.Schema;

namespace G2_SC603_KN_Proyecto.Models;

[NotMapped]
public class WodResumen
{
    public int IdRutina { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Objetivo { get; set; }
    public int IdEntrenador { get; set; }
    public string NombreEntrenador { get; set; } = null!;
    public int? IdRutinaEjercicio { get; set; }
    public string? NombreEjercicio { get; set; }
    public int? Series { get; set; }
    public int? Repeticiones { get; set; }
    public int? Descanso { get; set; }
}