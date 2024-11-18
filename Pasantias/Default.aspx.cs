using System;

namespace Pasantias
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si la sesión contiene los valores necesarios
            if (Session["PrimerNombre"] != null && Session["PrimerApellido"] != null)
            {
                string primerNombre = Session["PrimerNombre"].ToString();
                string primerApellido = Session["PrimerApellido"].ToString();

                // Asigna el texto del Label
                lblBienvenida.Text = $"{primerNombre} {primerApellido}";
            }
            else
            {
                // Redirige al Login si no hay sesión activa
                Response.Redirect("Login.aspx");
            }
        }
    }
}
