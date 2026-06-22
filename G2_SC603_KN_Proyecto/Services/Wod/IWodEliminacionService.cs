namespace G2_SC603_KN_Proyecto.Services.Wod
{
    /// <summary>
    /// Operación de eliminación de entrenamientos (RMGM-WOD-005).
    /// Se mantiene separada de <see cref="IWodConsultaService"/> (ISP):
    /// solo la acción administrativa de borrado depende de este contrato,
    /// las acciones de consulta no se ven obligadas a conocerlo.
    /// </summary>
    public interface IWodEliminacionService
    {
        /// <summary>
        /// Elimina una rutina (WOD) junto con sus dependencias
        /// (ejercicios asociados y asignaciones a clientes).
        /// </summary>
        /// <returns>
        /// Una tupla indicando si la operación tuvo éxito y un mensaje
        /// amigable para mostrar al usuario.
        /// </returns>
        Task<(bool Exito, string Mensaje)> EliminarRutinaAsync(int idRutina);
    }
}
