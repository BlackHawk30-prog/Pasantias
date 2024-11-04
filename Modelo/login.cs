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
        public bool VerificarCredenciales(string usuario, string password, out int rol)
        {
            bool credencialesValidas = false;  // Variable para almacenar el resultado
            rol = -1;  // Inicializa el rol con un valor de error o sin rol asignado
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
                MySqlParameter outputParam = new MySqlParameter("p_rol", MySqlDbType.Int32);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                try
                {
                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Comprobar si el valor de salida es DBNull antes de convertirlo
                    if (outputParam.Value != DBNull.Value)
                    {
                        // Obtener el rol del parámetro de salida
                        rol = Convert.ToInt32(outputParam.Value);

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
