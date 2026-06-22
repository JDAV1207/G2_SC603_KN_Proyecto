using G2_SC603_KN_Proyecto.Models;
using G2_SC603_KN_Proyecto.Filters;
using G2_SC603_KN_Proyecto.Models.ViewModels.Wod;
using G2_SC603_KN_Proyecto.Services.Wod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class WODController : Controller
    {
        private readonly DbOrionFitContext _context;
        private readonly IWodConsultaService _wodConsultaService;
        private readonly IWodEliminacionService _wodEliminacionService;

        public WODController(
            DbOrionFitContext context,
            IWodConsultaService wodConsultaService,
            IWodEliminacionService wodEliminacionService)
        {
            _context = context;
            _wodConsultaService = wodConsultaService;
            _wodEliminacionService = wodEliminacionService;
        }

        #region Mostrar WODs
        public async Task<IActionResult> MostrarWOD()
        {
            // Obtener los ejercicios utilizando el procedimiento almacenado
            List<EjercicioResumen> ejercicios = await _context.EjerciciosResumen
                .FromSqlRaw("CALL sp_ObtenerEjercicios()")
                .ToListAsync();
            // Obtener los WODs con sus ejercicios utilizando el procedimiento almacenado
            List<WodResumen> wods = await _context.WodsResumen
                .FromSqlRaw("CALL sp_ObtenerWODs()")
                .ToListAsync();

            ViewBag.Wods = wods;

            return View(ejercicios);
        }
        #endregion

        #region Agregar WOD
        [HttpPost]
        public async Task<IActionResult> AgregarWOD(string nombre, string objetivo, string ejerciciosJson)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    TempData["ErrorMessage"] = "El nombre del entrenamiento es obligatorio.";
                    return RedirectToAction("MostrarWOD");
                }

                if (string.IsNullOrWhiteSpace(ejerciciosJson) || ejerciciosJson == "[]")
                {
                    TempData["ErrorMessage"] = "Debe agregar al menos un ejercicio al WOD.";
                    return RedirectToAction("MostrarWOD");
                }

                // Obtener el id_entrenador desde la sesion del usuario logueado
                string usernameActual = HttpContext.Session.GetString("Usuario") ?? string.Empty;

                Entrenador? entrenador = await _context.Entrenadors
                    .Include(e => e.IdUsuarioNavigation)
                    .FirstOrDefaultAsync(e => e.IdUsuarioNavigation.Username == usernameActual);

                if (entrenador == null)
                {
                    // Si el usuario es ADMIN o no tiene entrenador, usar el primer entrenador disponible
                    entrenador = await _context.Entrenadors.FirstOrDefaultAsync()
                        ?? throw new Exception("No hay entrenadores registrados en el sistema.");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL sp_AgregarWOD({0}, {1}, {2}, {3})",
                    entrenador.IdEntrenador,
                    nombre,
                    objetivo ?? string.Empty,
                    ejerciciosJson
                );

                TempData["SuccessMessage"] = "WOD publicado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al publicar el WOD: " + ex.Message;
            }

            return RedirectToAction("MostrarWOD");
        }
        #endregion

        #region Editar WOD
        // DTO interno para deserializar el JSON de ejercicios
        private class EjercicioWodDto
        {
            public int IdEjercicio { get; set; }
            public int Series { get; set; }
            public int Repeticiones { get; set; }
            public int Descanso { get; set; }
        }

        // Cargar formulario con datos actuales
        [HttpGet]
        public async Task<IActionResult> EditarWOD(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "ADMIN" && rol != "TRAINER")
                return RedirectToAction("MostrarWOD");

            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null)
            {
                TempData["ErrorMessage"] = "El WOD no existe.";
                return RedirectToAction("MostrarWOD");
            }

            var ejerciciosRutina = await _context.RutinaEjercicios
                .Where(re => re.IdRutina == id)
                .Include(re => re.IdEjercicioNavigation)
                .ToListAsync();

            var todosEjercicios = await _context.EjerciciosResumen
                .FromSqlRaw("CALL sp_ObtenerEjercicios()")
                .ToListAsync();

            ViewBag.EjerciciosRutina = ejerciciosRutina;
            ViewBag.TodosEjercicios = todosEjercicios;
            return View(rutina);
        }

        // Guardar cambios / Validar
        [HttpPost]
        public async Task<IActionResult> EditarWOD(int idRutina, string nombre,
            string objetivo, string ejerciciosJson)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "ADMIN" && rol != "TRAINER")
                return RedirectToAction("MostrarWOD");

            //  Validar datos
            if (string.IsNullOrWhiteSpace(nombre))
            {
                TempData["ErrorMessage"] = "El nombre del entrenamiento es obligatorio.";
                return RedirectToAction("EditarWOD", new { id = idRutina });
            }
            if (string.IsNullOrWhiteSpace(ejerciciosJson) || ejerciciosJson == "[]")
            {
                TempData["ErrorMessage"] = "Debe agregar al menos un ejercicio.";
                return RedirectToAction("EditarWOD", new { id = idRutina });
            }

            try
            {
                // Actualizar rutina
                var rutina = await _context.Rutinas.FindAsync(idRutina);
                if (rutina == null)
                {
                    TempData["ErrorMessage"] = "El WOD no existe.";
                    return RedirectToAction("MostrarWOD");
                }

                rutina.Nombre = nombre;
                rutina.Objetivo = objetivo ?? string.Empty;

                var ejerciciosViejos = _context.RutinaEjercicios
                    .Where(re => re.IdRutina == idRutina);
                _context.RutinaEjercicios.RemoveRange(ejerciciosViejos);

                var lista = System.Text.Json.JsonSerializer
                    .Deserialize<List<EjercicioWodDto>>(ejerciciosJson);

                if (lista != null)
                {
                    foreach (var ej in lista)
                    {
                        _context.RutinaEjercicios.Add(new RutinaEjercicio
                        {
                            IdRutina = idRutina,
                            IdEjercicio = ej.IdEjercicio,
                            Series = ej.Series,
                            Repeticiones = ej.Repeticiones,
                            Descanso = ej.Descanso
                        });
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "WOD actualizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al actualizar: " + ex.Message;
                return RedirectToAction("EditarWOD", new { id = idRutina });
            }

            return RedirectToAction("MostrarWOD");
        }
        #region Eliminar WOD 
        // el id se valida y la confirmación ocurre en el cliente
        // (ver MostrarWOD.cshtml -> confirmarEliminar). Escenario 3: si se cancela
        // el modal de confirmación, este endpoint nunca se invoca.
        // Requiere POST + AntiForgeryToken + rol ADMIN únicamente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RolAutorizado("ADMIN")]
        public async Task<IActionResult> EliminarWOD(int id)
        {
            (bool exito, string mensaje) = await _wodEliminacionService.EliminarRutinaAsync(id);

            TempData[exito ? "SuccessMessage" : "ErrorMessage"] = mensaje;

            return RedirectToAction(nameof(MostrarWOD));
        }
        #endregion
        //Cancelar
        #endregion

        #region Entrenamiento Diario (RMGM-WOD-002)
        //  la vista decide qué mostrar según si la lista viene vacía.
        [HttpGet]
        public async Task<IActionResult> EntrenamientoDiario()
        {
            (int idUsuario, string rol) = ObtenerUsuarioActual();

            List<WodHistorialItemViewModel> entrenamientoDiario =
                await _wodConsultaService.ObtenerEntrenamientoDiarioAsync(idUsuario, rol);

            return View(entrenamientoDiario);
        }
        #endregion

        #region Historial de Entrenamientos (RMGM-WOD-003)
        // la vista decide qué mostrar según si la lista viene vacía.
        [HttpGet]
        public async Task<IActionResult> HistorialEntrenamientos()
        {
            (int idUsuario, string rol) = ObtenerUsuarioActual();

            List<WodHistorialItemViewModel> historial =
                await _wodConsultaService.ObtenerHistorialAsync(idUsuario, rol);

            return View(historial);
        }
        #endregion

        #region Detalle de Entrenamiento
        //  vista de detalle compartida,
        // ya que ambas historias piden lo mismo al seleccionar un entrenamiento.
        [HttpGet]
        public async Task<IActionResult> DetalleEntrenamiento(int id)
        {
            (int idUsuario, string rol) = ObtenerUsuarioActual();

            WodDetalleViewModel? detalle =
                await _wodConsultaService.ObtenerDetalleAsync(id, idUsuario, rol);

            if (detalle == null)
            {
                TempData["ErrorMessage"] = "El entrenamiento no existe o no tiene acceso a este registro.";
                return RedirectToAction(nameof(HistorialEntrenamientos));
            }

            return View(detalle);
        }
        #endregion

        /// Obtiene el id de usuario y el rol desde la sesión actual.
        /// Centraliza esta lectura para evitar duplicar el acceso a
        /// HttpContext.Session en cada acción (DRY).
      
        private (int IdUsuario, string Rol) ObtenerUsuarioActual()
        {
            int idUsuario = HttpContext.Session.GetInt32("ID") ?? 0;
            string rol = HttpContext.Session.GetString("Rol") ?? string.Empty;
            return (idUsuario, rol);
        }


    }
}