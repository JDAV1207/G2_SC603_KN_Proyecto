using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class ClienteMembresium
{
    public int IdClienteMembresia { get; set; }

    public int IdCliente { get; set; }

    public int IdMembresia { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Membresium IdMembresiaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
