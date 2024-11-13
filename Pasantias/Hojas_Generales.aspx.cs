using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Hojas_Generales : System.Web.UI.Page
    {
        HojasGenerales Hojas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hojas = new HojasGenerales();
                Hojas.grid_Generales(grid_Generales);
            }

        }
        protected void grid_Generales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obtener el IDHojaTiempo del CommandArgument
                string idHojaTiempo = e.CommandArgument.ToString();

                // Redirigir a la página Hoja_Tiempo con el IDHojaTiempo como parámetro en la URL
                Response.Redirect($"Hoja_Tiempo.aspx?IDHojaTiempo={idHojaTiempo}");
            }
        }

    }
}