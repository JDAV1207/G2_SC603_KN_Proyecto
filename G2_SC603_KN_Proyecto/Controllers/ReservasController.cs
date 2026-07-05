using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class ReservasController : Controller
    {
        private readonly DbOrionFitContext _context;

        public ReservasController(DbOrionFitContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string rol = HttpContext.Session.GetString("Rol") ?? string.Empty;
            int? idUsuario = HttpContext.Session.GetInt32("ID");

            Cliente? cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            int idCliente = cliente?.IdCliente ?? 0;

            ReservasViewModel model = new ReservasViewModel();
            model.EsAdmin = rol == "ADMIN" || rol == "RECEPTION";
            model.EsCliente = cliente != null;

            model.Clases = await _context.Clases
                .OrderBy(c => c.Horario)
                .Select(c => new ClaseDisponibleVM
                {
                    IdClase = c.IdClase,
                    Nombre = c.Nombre,
                    Entrenador = c.IdEntrenadorNavigation.Nombre,
                    Horario = c.Horario,
                    Cupo = c.Cupo,
                    Reservados = c.Reservas.Count(r => r.Estado == "Activa"),
                    Wod = c.IdRutinaNavigation != null ? c.IdRutinaNavigation.Nombre : null,
                    WodImagen = c.IdRutinaNavigation != null ? c.IdRutinaNavigation.Imagen : null,
                    YaReservada = idCliente != 0 && c.Reservas.Any(r =>
                        r.IdCliente == idCliente &&
                        r.Estado == "Activa")
                })
                .ToListAsync();

            if (cliente != null)
            {
                DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

                model.AsistenciaHoy = await _context.Asistencia.AnyAsync(a =>
                    a.IdCliente == idCliente &&
                    a.Fecha == hoy);

                model.MisReservas = await _context.Reservas
                    .Include(r => r.IdClaseNavigation)
                    .Where(r => r.IdCliente == idCliente && r.Estado == "Activa")
                    .OrderBy(r => r.IdClaseNavigation.Horario)
                    .ToListAsync();
            }

            if (model.EsAdmin)
            {
                ViewBag.Entrenadores = await _context.Entrenadors.ToListAsync();
                ViewBag.Wods = await _context.Rutinas.OrderByDescending(r => r.IdRutina).ToListAsync();

                model.TodasReservas = await _context.Reservas
                    .Include(r => r.IdClaseNavigation)
                    .Include(r => r.IdClienteNavigation)
                    .OrderByDescending(r => r.FechaReserva)
                    .ToListAsync();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CrearClase(string nombre, DateTime horario, int cupo, int idEntrenador, int? idRutina)
        {
            string rol = HttpContext.Session.GetString("Rol") ?? string.Empty;

            if (rol != "ADMIN" && rol != "RECEPTION")
            {
                TempData["ErrorMessage"] = "No tiene permisos para crear clases.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(nombre) || cupo <= 0)
            {
                TempData["ErrorMessage"] = "El nombre y un cupo mayor a cero son obligatorios.";
                return RedirectToAction("Index");
            }

            bool entrenadorExiste = await _context.Entrenadors.AnyAsync(e => e.IdEntrenador == idEntrenador);

            if (!entrenadorExiste)
            {
                TempData["ErrorMessage"] = "El entrenador seleccionado no existe.";
                return RedirectToAction("Index");
            }

            Clase clase = new Clase
            {
                Nombre = nombre.Trim(),
                Horario = horario,
                Cupo = cupo,
                IdEntrenador = idEntrenador,
                IdRutina = idRutina
            };

            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Clase creada correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reservar(int idClase)
        {
            int? idUsuario = HttpContext.Session.GetInt32("ID");

            Cliente? cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (cliente == null)
            {
                TempData["ErrorMessage"] = "Debe iniciar sesión como cliente para reservar.";
                return RedirectToAction("Index");
            }

            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

            bool membresiaActiva = await _context.ClienteMembresia.AnyAsync(cm =>
                cm.IdCliente == cliente.IdCliente &&
                cm.Estado == "Activa" &&
                cm.FechaFin >= hoy);

            if (!membresiaActiva)
            {
                TempData["ErrorMessage"] = "No cuenta con una membresía activa, no es posible reservar.";
                return RedirectToAction("Index");
            }

            Clase? clase = await _context.Clases
                .FirstOrDefaultAsync(c => c.IdClase == idClase);

            if (clase == null)
            {
                TempData["ErrorMessage"] = "La clase seleccionada no existe.";
                return RedirectToAction("Index");
            }

            bool yaReservada = await _context.Reservas.AnyAsync(r =>
                r.IdClase == idClase &&
                r.IdCliente == cliente.IdCliente &&
                r.Estado == "Activa");

            if (yaReservada)
            {
                TempData["ErrorMessage"] = "Ya tiene una reserva activa para esta clase.";
                return RedirectToAction("Index");
            }

            int reservasActivas = await _context.Reservas.CountAsync(r =>
                r.IdClase == idClase &&
                r.Estado == "Activa");

            // Cupo lleno
            if (reservasActivas >= clase.Cupo)
            {
                TempData["ErrorMessage"] = "La clase no tiene cupo disponible.";
                return RedirectToAction("Index");
            }

            Reserva reserva = new Reserva
            {
                IdCliente = cliente.IdCliente,
                IdClase = idClase,
                FechaReserva = hoy,
                Estado = "Activa"
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva registrada correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int idReserva)
        {
            Reserva? reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva && r.Estado == "Activa");

            if (reserva == null)
            {
                TempData["ErrorMessage"] = "No existe una reserva activa para cancelar.";
                return RedirectToAction("Index");
            }

            // Libera el cupo
            reserva.Estado = "Cancelada";

            // Revierte la asistencia del día
            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

            Asistencium? asistenciaHoy = await _context.Asistencia
                .FirstOrDefaultAsync(a => a.IdCliente == reserva.IdCliente && a.Fecha == hoy);

            if (asistenciaHoy != null)
            {
                _context.Asistencia.Remove(asistenciaHoy);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva cancelada, el cupo quedó disponible.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAsistencia(int idReserva)
        {
            Reserva? reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva && r.Estado == "Activa");

            if (reserva == null)
            {
                TempData["ErrorMessage"] = "No se encontró la reserva.";
                return RedirectToAction("Index");
            }

            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

            bool yaRegistrada = await _context.Asistencia.AnyAsync(a =>
                a.IdCliente == reserva.IdCliente &&
                a.Fecha == hoy);

            if (yaRegistrada)
            {
                TempData["ErrorMessage"] = "La asistencia de hoy ya fue registrada.";
                return RedirectToAction("Index");
            }

            Asistencium asistencia = new Asistencium
            {
                IdCliente = reserva.IdCliente,
                Fecha = hoy,
                HoraEntrada = TimeOnly.FromDateTime(DateTime.Now)
            };

            // Actualiza contador
            _context.Asistencia.Add(asistencia);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Asistencia registrada correctamente.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detalle(int id)
        {
            Reserva? reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdClaseNavigation)
                    .ThenInclude(c => c.IdEntrenadorNavigation)
                .Include(r => r.IdClaseNavigation)
                    .ThenInclude(c => c.IdRutinaNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                TempData["ErrorMessage"] = "La reserva no existe.";
                return RedirectToAction("Index");
            }

            return View(reserva);
        }
    }
}
