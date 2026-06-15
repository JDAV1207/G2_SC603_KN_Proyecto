using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class MembresiaController : Controller
    {
        private readonly DbOrionFitContext _context;

        public MembresiaController(DbOrionFitContext context)
        {
            _context = context;
        }

        #region Mostrar Membresia
        public async Task<IActionResult> MostrarMembresia()
        {
            List<ClienteMembresiaResumen> clientes = await _context.ClienteMembresiaResumen
                .FromSqlRaw("CALL sp_ObtenerClientesMembresias()")
                .ToListAsync();

            var listaclientes = _context.Clientes.ToList();
            var listamembresias = _context.Membresia.ToList();

            ViewBag.Clientes = listaclientes;
            ViewBag.Membresias = listamembresias;

            return View(clientes);
        }
        #endregion

        #region Agregar Membresia
        [HttpPost]
        public async Task<IActionResult> AgregarMembresia(ClienteMembresiaResumen nuevoCliente)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL sp_AgregarClienteMembresia({0}, {1}, {2}, {3}, {4})",
                    nuevoCliente.IdCliente,
                    nuevoCliente.IdMembresia,
                    nuevoCliente.FechaInicio,
                    nuevoCliente.FechaFin,
                    nuevoCliente.Estado
                );
                TempData["SuccessMessage"] = "Cliente agregado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al agregar el cliente: " + ex.Message;
            }

            return RedirectToAction("MostrarMembresia");
        }
        #endregion

        #region Editar Membresía
        [HttpPost]
        public async Task<IActionResult> EditarMembresia(ClienteMembresiaResumen clienteEditado)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL sp_ActualizarClienteMembresia({0}, {1}, {2}, {3}, {4})",
                    clienteEditado.IdCliente,
                    clienteEditado.IdMembresia,
                    clienteEditado.FechaInicio,
                    clienteEditado.FechaFin,
                    clienteEditado.Estado
                );
                TempData["SuccessMessage"] = "Cliente actualizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al editar el cliente: " + ex.Message;
            }

            return RedirectToAction("MostrarMembresia");
        }
        #endregion
    }
}
