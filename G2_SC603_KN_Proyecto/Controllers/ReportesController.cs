using G2_SC603_KN_Proyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class ReportesController : Controller
    {
        // GET: NotificacionesController
        public ActionResult Index(string estado = "")
        {
            var listamembresias = _context.Membresia.ToList();

            ViewBag.Membresias = listamembresias; 
            ViewData["Mensaje"] = "Notificaciones";
            var reporte = (from c in _context.Clientes
                           join cm in _context.ClienteMembresia
                               on c.IdCliente equals cm.IdCliente
                           join m in _context.Membresia
                               on cm.IdMembresia equals m.IdMembresia
                           join p in _context.Pagos
                                on cm.IdClienteMembresia equals p.IdClienteMembresia
                           select new ReporteAnalisis
                           {
                               IdCliente = c.IdCliente,
                               Cedula = c.Cedula,
                               NombreCliente = c.Nombre,
                               Telefono = c.Telefono,
                               Correo = c.Correo,
                               Membresia = m.Nombre,
                               Precio = m.Precio,
                               FechaInicio = cm.FechaInicio,
                               FechaFin = cm.FechaFin,
                               Estado = cm.Estado,
                               MetodoPago = p.MetodoPago,
                               FechaPago = p.FechaPago,
                               Monto = p.Monto
                           });

            if (!string.IsNullOrEmpty(estado))
            {
                if (estado != "Todos")
                {
                    reporte = reporte.Where(x => x.Estado == estado);
                }
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Reporte Analisis");

                    // Encabezados
                    ws.Cell(1, 1).Value = "Cédula";
                    ws.Cell(1, 2).Value = "Cliente";
                    ws.Cell(1, 3).Value = "Teléfono";
                    ws.Cell(1, 4).Value = "Correo";
                    ws.Cell(1, 5).Value = "Membresía";
                    ws.Cell(1, 6).Value = "Precio";
                    ws.Cell(1, 7).Value = "Fecha Inicio";
                    ws.Cell(1, 8).Value = "Fecha Fin";
                    ws.Cell(1, 9).Value = "Estado";
                    ws.Cell(1, 10).Value = "Metodo Pago";
                    ws.Cell(1, 11).Value = "Fecha Pago";
                    ws.Cell(1, 12).Value = "Monto";

                    int fila = 2;

                    foreach (var item in reporte)
                    {
                        ws.Cell(fila, 1).Value = item.Cedula;
                        ws.Cell(fila, 2).Value = item.NombreCliente;
                        ws.Cell(fila, 3).Value = item.Telefono;
                        ws.Cell(fila, 4).Value = item.Correo;
                        ws.Cell(fila, 5).Value = item.Membresia;
                        ws.Cell(fila, 6).Value = item.Precio;
                        ws.Cell(fila, 7).Value = item.FechaInicio.ToString();
                        ws.Cell(fila, 8).Value = item.FechaFin.ToString();
                        ws.Cell(fila, 9).Value = item.Estado;
                        ws.Cell(fila, 10).Value = item.MetodoPago;
                        ws.Cell(fila, 11).Value = item.FechaPago.ToString();
                        ws.Cell(fila, 12).Value = item.Monto;

                        fila++;
                    }

                    ws.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var contenido = stream.ToArray();

                        return File(
                            contenido,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Reporte_Analisis_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                    }

                }
            }

            return View(reporte.ToList());
        }

        private readonly DbOrionFitContext _context;

        public ReportesController(DbOrionFitContext context)
        {
            _context = context;
        }

        #region Reporte Membresia
        public IActionResult Membresia(string estado = "")
        {
            var reporte = (from c in _context.Clientes
                           join cm in _context.ClienteMembresia
                               on c.IdCliente equals cm.IdCliente
                           join m in _context.Membresia
                               on cm.IdMembresia equals m.IdMembresia
                           select new ReporteMembresiaViewModel
                           {
                               IdCliente = c.IdCliente,
                               Cedula = c.Cedula,
                               NombreCliente = c.Nombre,
                               Telefono = c.Telefono,
                               Correo = c.Correo,
                               Membresia = m.Nombre,
                               Precio = m.Precio,
                               FechaInicio = cm.FechaInicio,
                               FechaFin = cm.FechaFin,
                               Estado = cm.Estado
                           });

            if (!string.IsNullOrEmpty(estado))
            {
                if (estado != "Todos")
                {
                    reporte = reporte.Where(x => x.Estado == estado);
                }
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Reporte Membresías");

                    // Encabezados
                    ws.Cell(1, 1).Value = "Cédula";
                    ws.Cell(1, 2).Value = "Cliente";
                    ws.Cell(1, 3).Value = "Teléfono";
                    ws.Cell(1, 4).Value = "Correo";
                    ws.Cell(1, 5).Value = "Membresía";
                    ws.Cell(1, 6).Value = "Precio";
                    ws.Cell(1, 7).Value = "Fecha Inicio";
                    ws.Cell(1, 8).Value = "Fecha Fin";
                    ws.Cell(1, 9).Value = "Estado";

                    int fila = 2;

                    foreach (var item in reporte)
                    {
                        ws.Cell(fila, 1).Value = item.Cedula;
                        ws.Cell(fila, 2).Value = item.NombreCliente;
                        ws.Cell(fila, 3).Value = item.Telefono;
                        ws.Cell(fila, 4).Value = item.Correo;
                        ws.Cell(fila, 5).Value = item.Membresia;
                        ws.Cell(fila, 6).Value = item.Precio;
                        ws.Cell(fila, 7).Value = item.FechaInicio.ToString();
                        ws.Cell(fila, 8).Value = item.FechaFin.ToString();
                        ws.Cell(fila, 9).Value = item.Estado;

                        fila++;
                    }

                    ws.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var contenido = stream.ToArray();

                        return File(
                            contenido,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Reporte_Membresias_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                    }

                }
            }

            return View(reporte.ToList());
        }
        #endregion

        #region Reporte Cliente
        public IActionResult Cliente(string estado = "")
        {
            var reporte = (from c in _context.Clientes
                           select new Cliente
                           {
                               IdCliente = c.IdCliente,
                               Cedula = c.Cedula,
                               Nombre = c.Nombre,
                               Telefono = c.Telefono,
                               Correo = c.Correo,
                               Estado = c.Estado
                           });

            if (!string.IsNullOrEmpty(estado))
            {
                if (estado != "Todos")
                {
                    reporte = reporte.Where(x => x.Estado == estado);
                }
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Reporte Clientes");

                    // Encabezados
                    ws.Cell(1, 1).Value = "Cédula";
                    ws.Cell(1, 2).Value = "Cliente";
                    ws.Cell(1, 3).Value = "Teléfono";
                    ws.Cell(1, 4).Value = "Correo";
                    ws.Cell(1, 5).Value = "Estado";

                    int fila = 2;

                    foreach (var item in reporte)
                    {
                        ws.Cell(fila, 1).Value = item.Cedula;
                        ws.Cell(fila, 2).Value = item.Nombre;
                        ws.Cell(fila, 3).Value = item.Telefono;
                        ws.Cell(fila, 4).Value = item.Correo;
                        ws.Cell(fila, 5).Value = item.Estado;

                        fila++;
                    }

                    ws.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var contenido = stream.ToArray();

                        return File(
                            contenido,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Reporte_Clientes_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                    }

                }
            }

            return View(reporte.ToList());
        }
        #endregion
    }
}
