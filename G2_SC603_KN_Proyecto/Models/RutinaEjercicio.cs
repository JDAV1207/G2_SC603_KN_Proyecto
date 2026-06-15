using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class RutinaEjercicio
{
    public int IdRutinaEjercicio { get; set; }

    public int IdRutina { get; set; }

    public int IdReserva { get; set; }

    public int IdEjercicio { get; set; }

    public int Series { get; set; }

    public int Repeticiones { get; set; }

    public int? Descanso { get; set; }

    public virtual Ejercicio IdEjercicioNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual Rutina IdRutinaNavigation { get; set; } = null!;
}
