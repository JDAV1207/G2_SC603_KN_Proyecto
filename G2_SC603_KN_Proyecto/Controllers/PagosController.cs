using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class PagosController : Controller
    {
        private readonly DbOrionFitContext _context;

        public PagosController(DbOrionFitContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string rol = HttpContext.Session.GetString("Rol") ?? "";
            int? idUsuario = HttpContext.Session.GetInt32("ID");

            IQueryable<Pago> query = _context.Pagos
                .Include(p => p.IdClienteMembresiaNavigation)
                .ThenInclude(cm => cm.IdClienteNavigation);

            // Si es USER, filtrar solo sus pagos
            if (rol == "USER")
            {
                query = query.Where(p =>
                    p.IdClienteMembresiaNavigation.IdClienteNavigation.IdUsuario == idUsuario
                );
            }

            List<Pago> pagos = query.ToList();

            ViewBag.Membresias = _context.ClienteMembresia
                .Include(cm => cm.IdClienteNavigation)
                .ToList();

            ViewBag.ClientesVencidos = _context.ClienteMembresia
                .Include(cm => cm.IdClienteNavigation)
                .Where(cm => cm.FechaFin < DateOnly.FromDateTime(DateTime.Today))
                .ToList();

            return View(pagos);
        }

        [HttpPost]
        public IActionResult RegistrarPago(Pago pago)
        {
            _context.Pagos.Add(pago);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult HistorialCliente(int idCliente)
        {
            List<Pago> pagos = _context.Pagos
                .Include(p => p.IdClienteMembresiaNavigation)
                .Where(p => p.IdClienteMembresiaNavigation.IdCliente == idCliente)
                .ToList();

            return View(pagos);
        }
    }
}
