using G2_SC603_KN_Proyecto.Models.ViewModels.Wod;

namespace G2_SC603_KN_Proyecto.Services.Wod
{
    /// Operaciones de consulta (solo lectura) sobre los entrenamientos (WOD).
    /// Se mantiene separada de <see cref="IWodEliminacionService"/> siguiendo
    /// ISP: las acciones de solo lectura del controlador no dependen de
    /// métodos de escritura que no necesitan.

    public interface IWodConsultaService
    {
        /// <summary>
        /// RMGM-WOD-003: historial de entrenamientos, filtrado según el rol
        /// de quien consulta (Cliente ve los suyos, Entrenador ve los que él
        /// publicó, Administrador ve todos), ordenado por fecha descendente.
        /// </summary>
        Task<List<WodHistorialItemViewModel>> ObtenerHistorialAsync(int idUsuario, string rol);

        /// entrenamientos asignados/publicados para el día de hoy.
        Task<List<WodHistorialItemViewModel>> ObtenerEntrenamientoDiarioAsync(int idUsuario, string rol);


        /// Detalle completo de un entrenamiento, validando que el usuario
        /// tenga permiso para verlo según su rol.
        /// Retorna null si no existe o si el usuario no tiene acceso.
       
        Task<WodDetalleViewModel?> ObtenerDetalleAsync(int idRutina, int idUsuario, string rol);
    }
}
