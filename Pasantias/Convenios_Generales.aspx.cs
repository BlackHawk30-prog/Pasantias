using Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Convenios_Generales : System.Web.UI.Page
    {
        ConveniosGenerales modeloConvenios; // Renombramos la variable para evitar confusión.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                modeloConvenios = new ConveniosGenerales(); // Instancia correcta del modelo.
                modeloConvenios.grid_Convenios(grid_Convenios); // Invoca el método desde la instancia.
            }
        }
    }
}
