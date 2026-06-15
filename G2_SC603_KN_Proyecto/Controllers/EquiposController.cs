using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

public class EquiposController : Controller
{
    private readonly DbOrionFitContext _context;

    public EquiposController(DbOrionFitContext context)
    {
        _context = context;
    }

    public IActionResult Index(string buscar)
    {
        var equipos = _context.Equipo.AsQueryable();

        if (!string.IsNullOrEmpty(buscar))
        {
            buscar = buscar.ToLower();

            equipos = equipos.Where(e =>
                e.Nombre.ToLower().Contains(buscar) ||
                e.Estado.ToLower().Contains(buscar) ||
                "equipamiento".Contains(buscar));
        }

        return View(equipos.ToList());
    }

    [HttpPost]
    public IActionResult EditarEquipo(
    int idEquipo,
    string nombre,
    string estado,
    DateOnly? fechaCompra,
    decimal? costo
        )
    {
        try
        {
            _context.Database.ExecuteSqlRaw(
                "CALL SP_EditarEquipo({0}, {1}, {2}, {3}, {4})",
                idEquipo,
                nombre,
                estado,
                fechaCompra.HasValue ? fechaCompra.Value : (object)DBNull.Value,
                costo.HasValue ? costo.Value : (object)DBNull.Value
            );
            TempData["SuccessMessage"] =
                "Equipo actualizado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] =
                "Error al actualizar equipo: " + ex.Message;
        }

        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult AgregarEquipo(
    string Nombre,
    DateTime? FechaCompra,
    decimal? Costo)
    {
        try
        {
            string estado = "Disponible";

            _context.Database.ExecuteSqlRaw(
                "CALL SP_AgregarEquipo({0}, {1}, {2}, {3})",
                Nombre,
                estado,
                FechaCompra,
                Costo
            );

            TempData["MensajeExito"] = "Equipo agregado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Error: " + ex.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var equipo = _context.Equipo
            .FirstOrDefault(e => e.IdEquipo == id);

        if (equipo == null)
        {
            return NotFound();
        }

        return View(equipo);
    }

    [HttpPost]
    public IActionResult Activar(int id)
    {
        var equipo = _context.Equipo
            .FirstOrDefault(e => e.IdEquipo == id);

        if (equipo == null)
        {
            return NotFound();
        }

        equipo.Estado = "Disponible";

        _context.SaveChanges();

        TempData["MensajeExito"] =
            "Equipo activado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Desactivar(int id)
    {
        var equipo = _context.Equipo
            .FirstOrDefault(e => e.IdEquipo == id);

        if (equipo == null)
        {
            return NotFound();
        }

        equipo.Estado = "No Disponible";

        _context.SaveChanges();

        TempData["MensajeExito"] =
            "Equipo desactivado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult EliminarEquipo(int idEquipo)
    {
        try
        {
            _context.Database.ExecuteSqlRaw(
                "CALL SP_EliminarEquipo({0})",
                idEquipo
            );

            TempData["MensajeExito"] = "Equipo eliminado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Error al eliminar: " + ex.Message;
        }

        return RedirectToAction("Index");
    }
}