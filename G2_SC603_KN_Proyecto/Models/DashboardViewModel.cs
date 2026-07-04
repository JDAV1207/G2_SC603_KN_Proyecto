using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public class DashboardViewModel
{
    //Tarjetas
    public int ClientesActivos { get; set; }
    public decimal IngresosMes { get; set; }
    public int AsistenciaHoy { get; set; }
    public int MembresiasPorVencer { get; set; }

    //Gráfico semanal
    public List<AsistenciaSemanalVM> AsistenciaSemanal { get; set; }

    public List<AsistenciaSemanalVM> AsistenciaMensual { get; set; }

    //Alertas
    public List<VencimientoVM> Vencimientos { get; set; }

    //Ranking
    public List<RankingClienteVM> RankingClientes { get; set; }

    //Ingresos de hoy
    public decimal IngresosHoy { get; set; }
    public List<PagoHoyVM> PagosHoy { get; set; }
}

public class AsistenciaSemanalVM
{
    public string Dia { get; set; }
    public int Cantidad { get; set; }
}
public class VencimientoVM
{
    public string Cliente { get; set; }
    public DateOnly FechaFin { get; set; }
    public int DiasRestantes { get; set; }
}

public class RankingClienteVM
{
    public string Cliente { get; set; }
    public int Asistencias { get; set; }
}

public class PagoHoyVM
{
    public string Cliente { get; set; }
    public decimal Monto { get; set; }
    public string Metodo { get; set; }
}