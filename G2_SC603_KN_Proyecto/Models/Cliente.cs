using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;
    public string Cedula { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<ClienteMembresium> ClienteMembresia { get; set; } = new List<ClienteMembresium>();

    public virtual ICollection<ClienteRutina> ClienteRutinas { get; set; } = new List<ClienteRutina>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
