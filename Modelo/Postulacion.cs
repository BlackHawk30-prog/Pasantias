using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class Postulacion
    {
        ConexionBD conectar;

        private DataTable grid_aplicantes()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = "SELECT u.IDUsuario, u.Primer_Nombre, u.Primer_Apellido, u.DNI, u.Correo, du.Telefono " +
                              "FROM usuarios u " +
                              "JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario " +
                              "JOIN roles_usuarios ru ON u.IDUsuario = ru.IDUsuario " +
                              "JOIN roles r ON ru.IDRol = r.IDRol " +
                              "WHERE r.IDRol = 1;";
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        public void grid_aplicantes(GridView grid)
        {
            grid.DataSource = grid_aplicantes();
            grid.DataBind();
        }

        public void ActualizarEstadoUsuario(int idUsuario, string campo, int valor)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = $"UPDATE usuarios SET {campo} = @valor WHERE IDUsuario = @idUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@valor", valor);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);

            comando.ExecuteNonQuery();
            conectar.CerrarConexion();
        }
    }
}
