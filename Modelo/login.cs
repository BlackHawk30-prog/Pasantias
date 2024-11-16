using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modelo
{
    public class Login
    {
        ConexionBD conectar;

        // Método para verificar las credenciales
        public bool VerificarCredenciales(string usuario, string password, out int rol, out int idUsuario)
        {
            bool credencialesValidas = false;
            rol = -1;
            idUsuario = -1;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "VerificarCredenciales";

            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_usuario", usuario);
                cmd.Parameters.AddWithValue("p_password", password);

                MySqlParameter outputParamRol = new MySqlParameter("p_rol", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParamRol);

                MySqlParameter outputParamID = new MySqlParameter("p_id_usuario", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParamID);

                try
                {
                    cmd.ExecuteNonQuery();

                    if (outputParamRol.Value != DBNull.Value && outputParamID.Value != DBNull.Value)
                    {
                        rol = Convert.ToInt32(outputParamRol.Value);
                        idUsuario = Convert.ToInt32(outputParamID.Value);
                        credencialesValidas = rol != -1;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error al verificar las credenciales: " + ex.Message);
                }
            }

            conectar.CerrarConexion();
            return credencialesValidas;
        }

        // Método para obtener los datos adicionales del usuario
        public (string PrimerNombre, string PrimerApellido) ObtenerDatosUsuario(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string primerNombre = string.Empty;
            string primerApellido = string.Empty;

            string consulta = "SELECT Primer_Nombre, Primer_Apellido FROM usuarios WHERE IDUsuario = @IDUsuario";

            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            primerNombre = reader["Primer_Nombre"].ToString();
                            primerApellido = reader["Primer_Apellido"].ToString();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error al obtener los datos del usuario: " + ex.Message);
                }
            }

            conectar.CerrarConexion();
            return (primerNombre, primerApellido);
        }
    }
}
