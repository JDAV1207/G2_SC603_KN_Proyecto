using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public int IdEntrenador { get; set; }

    public int? IdRutina { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime Horario { get; set; }

    public int Cupo { get; set; }

    public virtual Entrenador IdEntrenadorNavigation { get; set; } = null!;

    public virtual Rutina? IdRutinaNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
