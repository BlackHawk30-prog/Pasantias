using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modelo
{
    public class Login
    {
        ConexionBD conectar;

        // Método para verificar las credenciales de login usando un procedimiento almacenado
        public bool VerificarCredenciales(string usuario, string password)
        {
            bool credencialesValidas = false;  // Variable para almacenar el resultado
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

                try
                {
                    // Ejecutar el procedimiento almacenado y verificar si se encuentra algún resultado
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Si el resultado es mayor que 0, significa que las credenciales son correctas
                    credencialesValidas = count > 0;
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
