using Modelo;
using System;
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

        protected void BtnNuevaHoja_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);

            // Llama al método crear para insertar una nueva hoja de tiempo
            Hojas = new HojasGenerales();
            int resultado = Hojas.crear(userID);

            if (resultado > 0)
            {
                // Redirige a Hoja_Tiempo.aspx con el IDUsuario actual como parámetro en la URL
                Response.Redirect($"Hoja_Tiempo.aspx?IDUsuario={userID}");
            }
            else
            {
                Response.Write("<script>alert('Error al crear la hoja de tiempo.');</script>");
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
