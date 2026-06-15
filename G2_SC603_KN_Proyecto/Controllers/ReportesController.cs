using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G2_SC603_KN_Proyecto.Controllers
{
    public class ReportesController : Controller
    {
        // GET: NotificacionesController
        public ActionResult Index()
        {
            ViewData["Mensaje"] = "Notificaciones";
            return View();
        }

    }
}
