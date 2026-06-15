using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class ClienteResumen
{
    public int id_cliente { get; set; }

    public int id_usuario { get; set; }

    public string Nombre { get; set; } = null!;
    public string Cedula { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateOnly? fecha_nacimiento { get; set; }

    public string Estado { get; set; } = null!;

    public string? EstadoMembresia { get; set; }

    public DateOnly? Vencimiento { get; set; }

}
