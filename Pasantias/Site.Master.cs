using System;
using System.Web.UI.HtmlControls;

namespace Pasantias
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        // Declaración de los controles HTML Anchor
       
        protected HtmlAnchor FinanzasNav;
        protected HtmlAnchor OficialNav;
        
        protected HtmlAnchor RHNav;
        protected HtmlAnchor SeguridadNav;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            // Establecer los href con ResolveClientUrl
          
            FinanzasNav.HRef = ResolveClientUrl("~/Finanzas.aspx");
            OficialNav.HRef = ResolveClientUrl("~/Oficial.aspx");
           
            RHNav.HRef = ResolveClientUrl("~/RH.aspx");
            SeguridadNav.HRef = ResolveClientUrl("~/Seguridad.aspx");
            HojaGNav.HRef = ResolveClientUrl("~/Hojas_Generales.aspx");
            ConvenioPersonal.HRef = ResolveClientUrl("~/Convenio_Personal.aspx");
            ConveniosGenerales.HRef = ResolveClientUrl("~/Convenios_Generales.aspx");
            PostulacionesRH.HRef = ResolveClientUrl("~/Postulaciones.aspx");
            PostulacionesS.HRef = ResolveClientUrl("~/Postulaciones_Seguridad.aspx");
            PostulacionesR.HRef = ResolveClientUrl("~/Postulaciones_Regional.aspx");
            // Oculta todos los módulos por defecto

            FinanzasNav.Visible = false;
            OficialNav.Visible = false;
            
            RHNav.Visible = false;
            SeguridadNav.Visible = false;
            HojaGNav.Visible = false;
            ConvenioPersonal.Visible= false;
            ConveniosGenerales.Visible= false;
            PostulacionesRH.Visible= false;
            PostulacionesS.Visible= false;
            PostulacionesR.Visible = false;
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
                    
                    HojaGNav.Visible = true;
                    ConvenioPersonal.Visible = true;
                    ConveniosGenerales.Visible = true;
                    PostulacionesRH.Visible = true;
                    PostulacionesS.Visible = true;
                    PostulacionesR.Visible = true;


                }
                else if (rol == 2)
                {
                    HojaGNav.Visible = true;

                    FinanzasNav.Visible = true;
                }
                else if (rol == 3)
                {
                    OficialNav.Visible = true;
                    
                }
                else if (rol == 4)
                {
                    RHNav.Visible = true;
                }
                else if (rol == 5)
                {
                    SeguridadNav.Visible = true;
                }
                else if (rol == 6)
                {
                    FinanzasNav.Visible = true;
                }
            }
        }
    }
}
