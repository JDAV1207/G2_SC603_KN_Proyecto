using System.ComponentModel.DataAnnotations.Schema;

namespace G2_SC603_KN_Proyecto.Models;

[NotMapped]
public class EjercicioResumen
{
    public int IdEjercicio { get; set; }
    public string Nombre { get; set; } = null!;
    public string? GrupoMuscular { get; set; }
}