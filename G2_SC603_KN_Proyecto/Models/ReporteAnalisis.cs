using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class ReporteAnalisis
{
    public int IdCliente { get; set; }

    public string Cedula { get; set; }

    public string NombreCliente { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }

    public string Membresia { get; set; }

    public decimal Precio { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string Estado { get; set; }

    public string MetodoPago { get; set; }
    public DateOnly FechaPago { get; set; }
    public decimal Monto { get; set; }

}
