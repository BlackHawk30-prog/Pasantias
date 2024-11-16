using Modelo;
using MySql.Data.MySqlClient;
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
            Hojas = new HojasGenerales();

            // Llama al método crear para insertar una nueva hoja de tiempo
            int resultado = Hojas.crear(userID);

            if (resultado > 0)
            {
                // Después de crear la hoja de tiempo, consulta el IDHojaTiempo más reciente del usuario
                int idHojaTiempo;
                using (var conectar = new ConexionBD())
                {
                    conectar.AbrirConexion();
                    string consulta = "SELECT IDHojaTiempo FROM hoja_tiempo WHERE IDUsuario = @IDUsuario ORDER BY IDHojaTiempo DESC LIMIT 1;\r\n";
                    MySqlCommand obtenerId = new MySqlCommand(consulta, conectar.conectar);
                    obtenerId.Parameters.AddWithValue("@IDUsuario", userID);
                    idHojaTiempo = Convert.ToInt32(obtenerId.ExecuteScalar());
                    SessionStore.HojaID = idHojaTiempo;
                    conectar.CerrarConexion();
                }

                // Redirige a Hoja_Tiempo.aspx con el IDHojaTiempo recién obtenido
                Response.Redirect($"Hoja_Tiempo.aspx?IDHojaTiempo={idHojaTiempo}", false);

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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Default.aspx");

        }
    }
}
