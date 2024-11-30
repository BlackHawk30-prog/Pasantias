using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class ConvenioPersonal
    {
        ConexionBD conectar;

        private DataTable grid_Convenio_personal()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Obtener el ID de usuario desde la sesión
            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

            // Usar el procedimiento almacenado
            using (MySqlCommand comando = new MySqlCommand("GetConvenioPersonal", conectar.conectar))
            {
                comando.CommandType = CommandType.StoredProcedure;

                // Agregar parámetro al procedimiento
                comando.Parameters.AddWithValue("p_UserID", idUsuario);

                // Ejecutar el procedimiento con MySqlDataAdapter
                using (MySqlDataAdapter query = new MySqlDataAdapter(comando))
                {
                    query.Fill(tabla);
                }
            }
            conectar.CerrarConexion();
            return tabla;
        }

        public void grid_Convenio_personal(GridView grid)
        {
            grid.DataSource = grid_Convenio_personal();
            grid.DataBind();
        }
    }
}
