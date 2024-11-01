using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace Modelo
{
    public class Aplicante2
    {
        ConexionBD conectar;

        public int Crear(string Telefono, string Direccion, string Grado_Academico, string Sexo, byte[] Foto, byte[] Curriculum, string DNI)
        {
            int filasAfectadas = 0;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                // 1. Buscar el IDUsuario basado en el DNI
                int idUsuario = ObtenerIDUsuarioPorDNI(DNI);

                if (idUsuario == 0)
                {
                    throw new Exception("No se encontró un usuario con el DNI proporcionado.");
                }

                string consulta = "CrearDatosUsuario";  // Procedimiento almacenado

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("p_Telefono", Telefono);
                    cmd.Parameters.AddWithValue("p_Direccion", Direccion);
                    cmd.Parameters.AddWithValue("p_Grado_Academico", Grado_Academico);
                    cmd.Parameters.AddWithValue("p_Sexo", Sexo);
                    cmd.Parameters.AddWithValue("p_Foto", Foto ?? new byte[0]);
                    cmd.Parameters.AddWithValue("p_Curriculum", Curriculum ?? new byte[0]);
                    cmd.Parameters.AddWithValue("p_IDUsuario", idUsuario);  // Relación basada en el DNI

                    filasAfectadas = cmd.ExecuteNonQuery();
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

            return filasAfectadas;
        }

        // Método para obtener el IDUsuario usando el DNI
        private int ObtenerIDUsuarioPorDNI(string dni)
        {
            int idUsuario = 0;

            string query = "SELECT IDUsuario FROM usuarios WHERE DNI = @DNI LIMIT 1";

            using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
            {
                cmd.Parameters.AddWithValue("@DNI", dni);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idUsuario = reader.GetInt32("IDUsuario");
                    }
                }
            }

            return idUsuario;
        }
    }
}
