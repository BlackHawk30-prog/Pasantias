using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Modelo
{
    public class Vista_HJ
    {
        ConexionBD conectar;

        public DataTable grid_hojas()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            int idHojaTiempo = SessionStore.HojaID; // Recuperar el ID desde la sesión

            using (MySqlCommand cmd = new MySqlCommand("GetDetalleHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", idHojaTiempo);

                using (MySqlDataAdapter query = new MySqlDataAdapter(cmd))
                {
                    query.Fill(tabla);
                }
            }

            conectar.CerrarConexion();
            return tabla;
        }

        public void grid_hojas(GridView grid)
        {
            grid.DataSource = grid_hojas();
            grid.DataBind();
        }
    }
}

