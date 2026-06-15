using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Ejercicio
{
    public int IdEjercicio { get; set; }

    public int? IdEquipo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? GrupoMuscular { get; set; }

    public string? Descripcion { get; set; }

    public virtual Equipo? IdEquipoNavigation { get; set; }

    public virtual ICollection<RutinaEjercicio> RutinaEjercicios { get; set; } = new List<RutinaEjercicio>();
}
