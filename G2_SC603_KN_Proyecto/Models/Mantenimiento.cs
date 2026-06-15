using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Mantenimiento
{
    public int IdMantenimiento { get; set; }

    public int IdEquipo { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Costo { get; set; }

    public string? Estado { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
