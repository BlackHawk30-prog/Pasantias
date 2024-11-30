using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class ConveniosGenerales
    {
        ConexionBD conectar;

        private DataTable grid_Convenios()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Usar el procedimiento almacenado
            using (MySqlCommand comando = new MySqlCommand("GetConveniosGenerales", conectar.conectar))
            {
                comando.CommandType = CommandType.StoredProcedure;

                // Ejecutar el procedimiento con MySqlDataAdapter
                using (MySqlDataAdapter query = new MySqlDataAdapter(comando))
                {
                    query.Fill(tabla);
                }
            }
            conectar.CerrarConexion();
            return tabla;
        }

        public void grid_Convenios(GridView grid)
        {
            grid.DataSource = grid_Convenios();
            grid.DataBind();
        }
    }
}
