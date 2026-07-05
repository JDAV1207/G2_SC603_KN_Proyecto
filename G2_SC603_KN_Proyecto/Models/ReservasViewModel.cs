using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public class ReservasViewModel
{
    public bool EsAdmin { get; set; }
    public bool EsCliente { get; set; }
    public bool AsistenciaHoy { get; set; }

    public List<ClaseDisponibleVM> Clases { get; set; } = new List<ClaseDisponibleVM>();

    public List<Reserva> MisReservas { get; set; } = new List<Reserva>();

    public List<Reserva> TodasReservas { get; set; } = new List<Reserva>();
}

public class ClaseDisponibleVM
{
    public int IdClase { get; set; }
    public string Nombre { get; set; }
    public string Entrenador { get; set; }
    public DateTime Horario { get; set; }
    public int Cupo { get; set; }
    public int Reservados { get; set; }
    public bool YaReservada { get; set; }
    public string? Wod { get; set; }
    public string? WodImagen { get; set; }

    public int Disponibles => Cupo - Reservados;
}
