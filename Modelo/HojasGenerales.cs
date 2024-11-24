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

            // Consulta SQL con un parámetro
            string consulta = @"
    SELECT ht.IDHojaTiempo, u.Primer_Nombre, u.IDUsuario, ht.Fecha
    FROM hoja_tiempo ht
    INNER JOIN usuarios u ON ht.IDUsuario = u.IDUsuario
    WHERE ht.IDUsuario = @UserID;";


            using (MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar))
            {
                // Agregar parámetro a la consulta
                comando.Parameters.AddWithValue("@UserID", idUsuario);

                // Ejecutar la consulta con MySqlDataAdapter
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

                // Consulta para insertar en la tabla hoja_tiempo con el IDUsuario de la sesión
                string strConsulta = @"INSERT INTO hoja_tiempo (IDUsuario, PConfirmado, OConfirmado, RHConfirmado, fecha) 
                               VALUES (@IDUsuario, 0, 0, 0, @Fecha);";

                MySqlCommand insertar = new MySqlCommand(strConsulta, conectar.conectar);
                insertar.Parameters.AddWithValue("@IDUsuario", userID);
                insertar.Parameters.AddWithValue("@Fecha", DateTime.Now);

                // Ejecuta la consulta
                no_ingreso = insertar.ExecuteNonQuery();
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
