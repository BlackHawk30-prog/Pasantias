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

            string consulta = string.Format("SELECT ht.IDConvenio, ht.Fecha_Inicio, ht.Fecha_Final, u.IDUsuario, u.Primer_Nombre" +
                " From convenio ht INNER JOIN usuarios u ON ht.IDUsuario = u.IDUsuario Where ht.IDUsuario = @UserID;;");
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


        public void grid_Convenio_personal(GridView grid)
        {
            grid.DataSource = grid_Convenio_personal();
            grid.DataBind();
        }
    }
}
