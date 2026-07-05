using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Rutina
{
    public int IdRutina { get; set; }

    public int IdEntrenador { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Objetivo { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual ICollection<ClienteRutina> ClienteRutinas { get; set; } = new List<ClienteRutina>();

    public virtual Entrenador IdEntrenadorNavigation { get; set; } = null!;

    public virtual ICollection<RutinaEjercicio> RutinaEjercicios { get; set; } = new List<RutinaEjercicio>();
}
