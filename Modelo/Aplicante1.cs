using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modelo
{
    public class Aplicante1
    {
        ConexionBD conectar;

        // Método para agregar un nuevo aplicante
        public int agregar(string Nombre1, string Nombre2, string Apellido1, string Apellido2, string DNI, string Correo)
        {
            int no = 0;  // Variable que almacena el número de filas afectadas
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "AgregarUsuario";  // Nombre del procedimiento almacenado

            // Crear el comando y configurarlo como procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Indicar que es un procedimiento almacenado

                // Agregar solo los parámetros necesarios
                cmd.Parameters.AddWithValue("p_Nombre1", Nombre1);
                cmd.Parameters.AddWithValue("p_Nombre2", Nombre2);
                cmd.Parameters.AddWithValue("p_Apellido1", Apellido1);
                cmd.Parameters.AddWithValue("p_Apellido2", Apellido2);
                cmd.Parameters.AddWithValue("p_DNI", DNI);
                cmd.Parameters.AddWithValue("p_Correo", Correo);

                try
                {
                    // Ejecutar el procedimiento almacenado
                    no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
                }
                catch (MySqlException ex)
                {
                    // Manejo de excepciones si el DNI ya existe
                    if (ex.Message.Contains("El DNI ya existe"))
                    {
                        return -1; // Retorna -1 si el DNI ya existe
                    }
                    throw; // Rethrow para manejar otras excepciones
                }
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        // Método para verificar si el DNI ya existe
        public bool ExisteDNI(string dni)
        {
            int count = 0;
            using (var conectar = new ConexionBD())
            {
                conectar.AbrirConexion();
                string consulta = "SELECT COUNT(*) FROM usuarios WHERE DNI = @DNI";

                using (var cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count > 0; // Retorna true si el DNI existe
        }
    }
}
