using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class ClienteMembresiaResumen
{
    public string Cliente { get; set; }

    public string TipoPlan { get; set; }


    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? Precio { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdCliente { get; set; }

    public int? IdMembresia { get; set; }

}
