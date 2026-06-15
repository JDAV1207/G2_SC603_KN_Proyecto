using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateOnly? FechaCompra { get; set; }

    public decimal? Costo { get; set; }

    public virtual ICollection<Ejercicio> Ejercicios { get; set; } = new List<Ejercicio>();

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
