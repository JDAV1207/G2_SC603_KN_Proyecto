using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DashboardController : Controller
{
    private readonly DbOrionFitContext _context;

    public DashboardController(DbOrionFitContext context)
    {
        _context = context;
    }
    public IActionResult Dashboard()
    {
        DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

        DateOnly inicioMes = new DateOnly(
            hoy.Year,
            hoy.Month,
            1);

        DashboardViewModel model = new DashboardViewModel();

        //Clientes activos
        model.ClientesActivos = _context.Clientes
                    .Count(c => c.Estado == "Activo");

        //Ingresos del mes
        model.IngresosMes = _context.Pagos
                .Where(p => p.FechaPago >= inicioMes)
                .Sum(p => (decimal?)p.Monto) ?? 0;

        //Asistencia de hoy
        model.AsistenciaHoy = _context.Asistencia
                .Count(a => a.Fecha == hoy);

        //Membresías por vencer
        model.MembresiasPorVencer = _context.ClienteMembresia
            .Count(c =>
                    c.FechaFin >= hoy &&
                    c.FechaFin <= hoy.AddDays(7));

        //Gráfico semanal
        model.AsistenciaSemanal = Enumerable.Range(0, 7)
            .Select(i =>
            {
                var fecha = hoy.AddDays(-6 + i);

                return new AsistenciaSemanalVM
                {
                    Dia = fecha.ToString("ddd"),
                    Cantidad = _context.Asistencia
                        .Count(a => a.Fecha == fecha)
                };
            }).ToList();

        // Asistencia del último mes (30 días)
        model.AsistenciaMensual = Enumerable.Range(0, 30)
            .Select(i =>
            {
                var fecha = hoy.AddDays(-29 + i);

                return new AsistenciaSemanalVM
                {
                    Dia = fecha.ToString("dd/MM"),
                    Cantidad = _context.Asistencia.Count(a => a.Fecha == fecha)
                };
            }).ToList();

        //Alertas de vencimiento
        model.Vencimientos = _context.ClienteMembresia
            .Include(x => x.IdClienteNavigation)
            .Where(x => x.FechaFin >= hoy &&
                        x.FechaFin <= hoy.AddDays(7))
            .OrderBy(x => x.FechaFin)
            .Select(x => new VencimientoVM
            {
                Cliente = x.IdClienteNavigation.Nombre,
                FechaFin = x.FechaFin,
                DiasRestantes = EF.Functions.DateDiffDay(hoy, x.FechaFin)
            }).ToList();

        //Top clientes
        model.RankingClientes = _context.Asistencia
            .Include(a => a.IdClienteNavigation)
            .GroupBy(a => a.IdClienteNavigation.Nombre)
            .Select(x => new RankingClienteVM
            {
                Cliente = x.Key,
                Asistencias = x.Count()
            })
            .OrderByDescending(x => x.Asistencias)
            .Take(5)
            .ToList();

        //Ingresos de hoy
        model.IngresosHoy = _context.Pagos
            .Where(x => x.FechaPago == hoy)
            .Sum(x => (decimal?)x.Monto) ?? 0;

        model.PagosHoy = _context.Pagos
            .Include(p => p.IdClienteMembresiaNavigation)
                .ThenInclude(cm => cm.IdClienteNavigation)
            .Where(p => p.FechaPago == hoy)
            .Select(p => new PagoHoyVM
            {
                Cliente = p.IdClienteMembresiaNavigation.IdClienteNavigation.Nombre,
                Monto = p.Monto,
                Metodo = p.MetodoPago
            }).ToList();

        return View(model);
    }
}