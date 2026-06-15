using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Asistencium
{
    public int IdAsistencia { get; set; }

    public int IdCliente { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
