using System;
using System.Web;
using System.Web.UI;

namespace Pasantias
{
    public partial class CerrarSesion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["UserID"] != null)
            {
                // Limpiar todas las variables de sesión
                Session.Clear();

                // Abandonar la sesión actual
                Session.Abandon();

                // Redirigir al usuario a la página de inicio de sesión
                Response.Redirect("~/login.aspx", false);
            }
            else
            {
                // Si no hay sesión activa, redirigir directamente al inicio de sesión
                Response.Redirect("~/login.aspx", false);
            }
        }
    }
}
