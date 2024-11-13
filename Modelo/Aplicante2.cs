using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace Modelo
{
    public class Aplicante2
    {
        ConexionBD conectar;

        // Método para crear datos del usuario
        public int Crear(DateTime FechaNacimiento, string Telefono, string Direccion, string Grado_Academico, string Sexo, byte[] Foto, byte[] Curriculum, string DNI)
        {
            int filasAfectadas = 0;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                // Buscar el IDUsuario basado en el DNI
                int idUsuario = ObtenerIDUsuarioPorDNI(DNI);
                if (idUsuario == 0)
                {
                    throw new Exception("No se encontró un usuario con el DNI proporcionado.");
                }

                // Guardar foto y currículum en el sistema de archivos y obtener las rutas
                string fotoPath = GuardarArchivoLocal(Foto, "Fotos", DNI + "_foto.jpg");
                string curriculumPath = GuardarArchivoLocal(Curriculum, "Curriculum", DNI + "_curriculum.pdf");

                // Procedimiento almacenado
                string consulta = "CrearDatosUsuario";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("p_Fecha_Nacimiento", FechaNacimiento);
                    cmd.Parameters.AddWithValue("p_Telefono", Telefono);
                    cmd.Parameters.AddWithValue("p_Direccion", Direccion);
                    cmd.Parameters.AddWithValue("p_Grado_Academico", Grado_Academico);
                    cmd.Parameters.AddWithValue("p_Sexo", Sexo);
                    cmd.Parameters.AddWithValue("p_Foto", fotoPath);
                    cmd.Parameters.AddWithValue("p_Curriculum", curriculumPath);
                    cmd.Parameters.AddWithValue("p_IDUsuario", idUsuario);

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

        // Método para guardar el archivo en el sistema de archivos y devolver la ruta
        private string GuardarArchivoLocal(byte[] archivo, string carpeta, string nombreArchivo)
        {
            if (archivo == null || archivo.Length == 0) return null;

            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, carpeta);

            // Crear el directorio si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            string rutaCompleta = Path.Combine(directorio, nombreArchivo);
            File.WriteAllBytes(rutaCompleta, archivo);

            // Devolver solo la ruta relativa para guardar en la base de datos
            return Path.Combine(carpeta, nombreArchivo);
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
