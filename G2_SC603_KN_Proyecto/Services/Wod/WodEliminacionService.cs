using G2_SC603_KN_Proyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Services.Wod
{
    /// <summary>
    /// Implementación de <see cref="IWodEliminacionService"/>.
    /// Responsabilidad única (SRP): eliminar de forma segura una rutina y
    /// sus dependencias, manejando los posibles errores de base de datos
    /// para que el controlador no deba conocer detalles de EF Core.
    /// </summary>
    public class WodEliminacionService : IWodEliminacionService
    {
        private readonly DbOrionFitContext _context;

        public WodEliminacionService(DbOrionFitContext context)
        {
            _context = context;
        }

        public async Task<(bool Exito, string Mensaje)> EliminarRutinaAsync(int idRutina)
        {
            Rutina? rutina = await _context.Rutinas.FindAsync(idRutina);

            if (rutina == null)
            {
                return (false, "El entrenamiento no existe o ya fue eliminado.");
            }

            try
            {
                // Las tablas rutina_ejercicio y cliente_rutina referencian a
                // rutina sin "ON DELETE CASCADE", por lo que sus dependencias
                // deben eliminarse explícitamente antes de borrar la rutina
                // para evitar una violación de llave foránea.
                IQueryable<RutinaEjercicio> ejerciciosAsociados = _context.RutinaEjercicios
                    .Where(re => re.IdRutina == idRutina);
                _context.RutinaEjercicios.RemoveRange(ejerciciosAsociados);

                IQueryable<ClienteRutina> asignacionesCliente = _context.ClienteRutinas
                    .Where(cr => cr.IdRutina == idRutina);
                _context.ClienteRutinas.RemoveRange(asignacionesCliente);

                _context.Rutinas.Remove(rutina);

                await _context.SaveChangesAsync();

                return (true, "Entrenamiento eliminado correctamente.");
            }
            catch (DbUpdateException)
            {
                return (false, "No se pudo eliminar el entrenamiento porque tiene datos relacionados.");
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error inesperado al eliminar el entrenamiento: " + ex.Message);
            }
        }
    }
}
