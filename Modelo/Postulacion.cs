using MySql.Data.MySqlClient;
using System;
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
        public string ObtenerDNIporIDUsuario(int idUsuario)
        {
            string dni = null;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                string consulta = "SELECT DNI FROM usuarios WHERE IDUsuario = @IDUsuario LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);

                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        dni = resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return dni;
        }
        // Método para obtener el correo basado en el DNI usando el procedimiento almacenado
        public string ObtenerCorreoPorDNI(string dni)
        {
            string correo = null;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                // Consulta SQL directa para obtener el correo basado en el DNI
                string consulta = "SELECT correo FROM usuarios WHERE DNI = @DNI LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);

                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        correo = resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return correo;
        }
    }
}
