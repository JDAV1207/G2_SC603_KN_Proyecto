using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class WODController : Controller
    {
        private readonly DbOrionFitContext _context;

        public WODController(DbOrionFitContext context)
        {
            _context = context;
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
    }
}