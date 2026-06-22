using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace G2_SC603_KN_Proyecto.Filters
{
    /// Filtro de autorización basado en el rol guardado en sesión.
    /// El proyecto no usa ASP.NET Identity ni cookies de autenticación, por
    /// lo que el atributo [Authorize] de Microsoft no tiene efecto real
    /// aquí; este filtro sigue el mismo mecanismo de sesión que ya usa el
    /// resto de los controladores (HttpContext.Session.GetString("Rol")).
    ///
    /// Reutilizable para cualquier acción que deba restringirse a uno o
    /// varios roles, simplemente pasando los roles permitidos (OCP: se
    /// extiende sin modificar el filtro).
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RolAutorizadoAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string[] _rolesPermitidos;

        public RolAutorizadoAttribute(params string[] rolesPermitidos)
        {
            _rolesPermitidos = rolesPermitidos;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string rolActual = context.HttpContext.Session.GetString("Rol") ?? string.Empty;

            if (!_rolesPermitidos.Contains(rolActual))
            {
                if (context.Controller is Controller controller)
                {
                    controller.TempData["ErrorMessage"] = "No tiene permisos para realizar esta acción.";
                }

                context.Result = new RedirectToActionResult("MostrarWOD", "WOD", null);
                return;
            }

            await next();
        }
    }
}
