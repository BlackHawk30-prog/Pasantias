using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Convenio_Personal : System.Web.UI.Page
    {
        ConvenioPersonal Convenio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Convenio = new ConvenioPersonal(); // Instancia correcta del modelo.
               Convenio.grid_Convenio_personal(grid_Convenio_personal); // Invoca el método desde la instancia.
            }

        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Default.aspx");

        }
    }
}