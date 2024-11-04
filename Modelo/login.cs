using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web;

namespace Modelo
{
    public class Login
    {
        ConexionBD conectar;

        // Método para verificar las credenciales de login y obtener el rol del usuario
        public bool VerificarCredenciales(string usuario, string password, out int rol, out int idUsuario)
        {
            bool credencialesValidas = false;  // Variable para almacenar el resultado
            rol = -1;  // Inicializa el rol con un valor de error o sin rol asignado
            idUsuario = -1;  // Inicializa el ID del usuario con un valor por defecto
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Nombre del procedimiento almacenado
            string consulta = "VerificarCredenciales";

            // Crear el comando para ejecutar el procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Especificar que se trata de un procedimiento almacenado

                // Agregar los parámetros necesarios
                cmd.Parameters.AddWithValue("p_usuario", usuario);
                cmd.Parameters.AddWithValue("p_password", password);

                // Parámetro de salida para el rol
                MySqlParameter outputParamRol = new MySqlParameter("p_rol", MySqlDbType.Int32);
                outputParamRol.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParamRol);

                // Parámetro de salida para el ID del usuario
                MySqlParameter outputParamID = new MySqlParameter("p_id_usuario", MySqlDbType.Int32);
                outputParamID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParamID);

                try
                {
                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Comprobar si los valores de salida no son DBNull antes de convertirlos
                    if (outputParamRol.Value != DBNull.Value && outputParamID.Value != DBNull.Value)
                    {
                        // Obtener el rol y el ID del usuario de los parámetros de salida
                        rol = Convert.ToInt32(outputParamRol.Value);
                        idUsuario = Convert.ToInt32(outputParamID.Value);

                        // Si el rol no es -1, significa que las credenciales son correctas
                        credencialesValidas = rol != -1;
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejo de excepciones en caso de errores con la base de datos
                    Console.WriteLine("Error al verificar las credenciales: " + ex.Message);
                }
            }

            conectar.CerrarConexion();
            return credencialesValidas;  // Devolver true si las credenciales son correctas, false en caso contrario
        }

    }
}
