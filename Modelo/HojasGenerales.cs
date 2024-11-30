using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;


namespace Modelo
{
    public class HojasGenerales
    {
        ConexionBD conectar;
        private DataTable grid_Generales()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Obtener el ID de usuario desde la sesión
            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

            // Llamar al procedimiento almacenado
            using (MySqlCommand comando = new MySqlCommand("GetHojasGenerales", conectar.conectar))
            {
                comando.CommandType = CommandType.StoredProcedure;
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


        public void grid_Generales(GridView grid)
        {
            grid.DataSource = grid_Generales();
            grid.DataBind();
        }
        public int crear(int userID)
        {
            int no_ingreso = 0;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                // Llamar al procedimiento almacenado para insertar
                using (MySqlCommand insertar = new MySqlCommand("InsertHojaTiempo", conectar.conectar))
                {
                    insertar.CommandType = CommandType.StoredProcedure;
                    insertar.Parameters.AddWithValue("p_UserID", userID);
                    insertar.Parameters.AddWithValue("p_Fecha", DateTime.Now);

                    // Ejecuta el procedimiento
                    no_ingreso = insertar.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar en hoja_tiempo: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return no_ingreso;
        }



    }
}
