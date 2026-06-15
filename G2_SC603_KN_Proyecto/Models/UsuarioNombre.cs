using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class UsuarioNombre
{

    public string Username { get; set; } = null!;

    public string? Nombre { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public string? Telefono { get; set; } = null!;

    public string? Correo { get; set; } = null!;
}
