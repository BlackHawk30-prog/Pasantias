using System;
using System.Web.UI.HtmlControls;

namespace Pasantias
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        // Declaración de los controles HTML Anchor
        protected HtmlAnchor HojaTiempoNav;
        protected HtmlAnchor FinanzasNav;
        protected HtmlAnchor OficialNav;
        protected HtmlAnchor PostulacionesNav;
        protected HtmlAnchor RHNav;
        protected HtmlAnchor SeguridadNav;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Establecer los href con ResolveClientUrl
            HojaTiempoNav.HRef = ResolveClientUrl("~/Hoja_Tiempo.aspx");
            FinanzasNav.HRef = ResolveClientUrl("~/Finanzas.aspx");
            OficialNav.HRef = ResolveClientUrl("~/Oficial.aspx");
            PostulacionesNav.HRef = ResolveClientUrl("~/Postulaciones.aspx");
            RHNav.HRef = ResolveClientUrl("~/RH.aspx");
            SeguridadNav.HRef = ResolveClientUrl("~/Seguridad.aspx");
            HojaGNav.HRef = ResolveClientUrl("~/Hojas_Generales.aspx");

            // Oculta todos los módulos por defecto
            HojaTiempoNav.Visible = false;
            FinanzasNav.Visible = false;
            OficialNav.Visible = false;
            PostulacionesNav.Visible = false;
            RHNav.Visible = false;
            SeguridadNav.Visible = false;
            HojaGNav.Visible = false;

            // Depuración temporal para verificar el rol en sesión
           // if (Session["UserRol"] == null)
          //  {
          //      Response.Write("No hay rol en la sesión.");
          //  }
          //  else
          //  {
          //      Response.Write("Rol en sesión: " + Session["UserRol"]);
          //  }

            // Verifica si el rol está en la sesión
            if (Session["UserRol"] != null)
            {
                int rol = int.Parse(Session["UserRol"].ToString());

                // Controla la visibilidad de cada módulo según el rol
                if (rol == 1)
                {
                    HojaTiempoNav.Visible = true;
                    PostulacionesNav.Visible = true;
                    OficialNav.Visible = true;
                    HojaGNav.Visible= true;
                }
                else if (rol == 2)
                {
                    HojaTiempoNav.Visible = true;
                    FinanzasNav.Visible = true;
                }
                else if (rol == 3)
                {
                    OficialNav.Visible = true;
                    HojaTiempoNav.Visible = true;
                }
                else if (rol == 6)
                {
                    RHNav.Visible = true;
                }
                else if (rol == 9)
                {
                    SeguridadNav.Visible = true;
                }
            }
        }
    }
}
