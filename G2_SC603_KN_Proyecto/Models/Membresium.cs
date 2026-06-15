using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Membresium
{
    public int IdMembresia { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int DuracionDias { get; set; }

    public virtual ICollection<ClienteMembresium> ClienteMembresia { get; set; } = new List<ClienteMembresium>();
}
