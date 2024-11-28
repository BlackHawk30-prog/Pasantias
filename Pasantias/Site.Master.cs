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
            // Verifica y asigna valores a los controles solo si no son null
            if (FinanzasNav != null)
                FinanzasNav.HRef = ResolveClientUrl("~/Finanzas.aspx");

            if (OficialNav != null)
                OficialNav.HRef = ResolveClientUrl("~/Oficial.aspx");

            if (RHNav != null)
                RHNav.HRef = ResolveClientUrl("~/RH.aspx");

            if (SeguridadNav != null)
                SeguridadNav.HRef = ResolveClientUrl("~/Seguridad.aspx");

            if (HojaGNav != null)
                HojaGNav.HRef = ResolveClientUrl("~/Hojas_Generales.aspx");

            if (ConvenioPersonal != null)
                ConvenioPersonal.HRef = ResolveClientUrl("~/Convenio_Personal.aspx");

            if (ConveniosGenerales != null)
                ConveniosGenerales.HRef = ResolveClientUrl("~/Convenios_Generales.aspx");

            if (PostulacionesRH != null)
                PostulacionesRH.HRef = ResolveClientUrl("~/Postulaciones.aspx");

            if (PostulacionesS != null)
                PostulacionesS.HRef = ResolveClientUrl("~/Postulaciones_Seguridad.aspx");

            if (PostulacionesR != null)
                PostulacionesR.HRef = ResolveClientUrl("~/Postulaciones_Regional.aspx");

            // Ocultar módulos por defecto
            SetControlVisibility(FinanzasNav, false);
            SetControlVisibility(OficialNav, false);
            SetControlVisibility(RHNav, false);
            SetControlVisibility(SeguridadNav, false);
            SetControlVisibility(HojaGNav, false);
            SetControlVisibility(ConvenioPersonal, false);
            SetControlVisibility(ConveniosGenerales, false);
            SetControlVisibility(PostulacionesRH, false);
            SetControlVisibility(PostulacionesS, false);
            SetControlVisibility(PostulacionesR, false);

            // Verifica el rol en sesión
            if (Session["UserRol"] != null)
            {
                int rol = int.Parse(Session["UserRol"].ToString());

                // Controla la visibilidad de cada módulo según el rol
                if (rol == 1 || rol == 2)
                {
                    SetControlVisibility(HojaGNav, true);
                    SetControlVisibility(ConvenioPersonal, true);
                    SetControlVisibility(ConveniosGenerales, true);
                    SetControlVisibility(PostulacionesRH, true);
                    SetControlVisibility(PostulacionesS, true);
                    SetControlVisibility(PostulacionesR, true);
                }
                else if (rol == 3)
                {
                    SetControlVisibility(OficialNav, true);
                }
                else if (rol == 4)
                {
                    SetControlVisibility(RHNav, true);
                }
                else if (rol == 5)
                {
                    SetControlVisibility(SeguridadNav, true);
                }
                else if (rol == 6)
                {
                    SetControlVisibility(FinanzasNav, true);
                }
            }
        }

        // Método auxiliar para controlar la visibilidad
        private void SetControlVisibility(HtmlAnchor control, bool isVisible)
        {
            if (control != null)
            {
                control.Visible = isVisible;
            }
        }

    }
}
