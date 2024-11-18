using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class Postulacion
    {
        ConexionBD conectar;

        private DataTable grid_aplicantes(string condicionAdicional = "")
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Construcción de la consulta con una posible condición adicional
            string consulta = "SELECT u.IDUsuario, u.Primer_Nombre, u.Primer_Apellido, u.DNI, u.Correo, du.Telefono " +
                              "FROM usuarios u " +
                              "JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario " +
                              "JOIN roles_usuarios ru ON u.IDUsuario = ru.IDUsuario " +
                              "JOIN roles r ON ru.IDRol = r.IDRol " +
                              "WHERE r.IDRol = 1 AND u.Eliminado = 0";

            if (!string.IsNullOrWhiteSpace(condicionAdicional))
            {
                consulta += " AND " + condicionAdicional;
            }

            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        public void grid_aplicantes(GridView grid, string condicionAdicional = "")
        {
            grid.DataSource = grid_aplicantes(condicionAdicional);
            grid.DataBind();
        }

        public void AceptarPostulacion(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE usuarios SET RHConfirmado = 1 WHERE IDUsuario = @IDUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }
        public void AceptarPostulacionSeguridad(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE usuarios SET SConfirmado = 1 WHERE IDUsuario = @IDUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }
        public void AceptarPostulacionRegional(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE usuarios SET RConfirmado = 1 WHERE IDUsuario = @IDUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }

        public void RechazarPostulacion(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE usuarios SET Eliminado = 1 WHERE IDUsuario = @IDUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }
    }
}
