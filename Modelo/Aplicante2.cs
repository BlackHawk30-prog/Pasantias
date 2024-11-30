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
        public int Crear(DateTime FechaNacimiento, string Telefono, string Cuenta, string Direccion, string Grado_Academico, string Sexo, byte[] Foto, byte[] Curriculum, string DNI)
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
                    cmd.Parameters.AddWithValue("p_Cuenta", Cuenta);
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

            // Obtener la extensión del archivo desde el nombre proporcionado
            string extension = Path.GetExtension(nombreArchivo);
            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("El archivo no tiene una extensión válida.");
            }

            // Definir el directorio y nombre completo del archivo a guardar
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, carpeta);

            // Crear el directorio si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            // Concatenar el nombre y la extensión para formar el nombre completo del archivo
            string rutaCompleta = Path.Combine(directorio, nombreArchivo);

            // Guardar el archivo en el sistema de archivos
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



        private bool ExisteMunicipio(string codigoMun)
        {
            bool existe = false;

            try
            {
                conectar = new ConexionBD();
                conectar.AbrirConexion();

                string consulta = "SELECT COUNT(*) FROM Municipios WHERE CodigoMun = @CodigoMun";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@CodigoMun", codigoMun);
                    existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
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

            return existe;
        }

        // Método para insertar un registro en la tabla Residencia
        public int InsertarResidencia(string dni, string municipio)
        {
            int filasAfectadas = 0;
            conectar = new ConexionBD();
            if (!ExisteMunicipio(municipio))
            {
                throw new Exception("El municipio seleccionado no es válido.");
            }

            try
            {
                conectar.AbrirConexion();

                // Obtener el IDUsuario basado en el DNI
                int idUsuario = ObtenerIDUsuarioPorDNI(dni);

                if (idUsuario == 0)
                {
                    throw new Exception("No se encontró un usuario con el DNI proporcionado.");
                }

                // Consulta SQL para insertar en la tabla Residencia
                string consulta = "INSERT INTO residencia (IDUsuario, CodigoMun) VALUES (@IDUsuario, @Municipio)";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    // Asignar valores a los parámetros
                    cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Municipio", municipio);


                    // Ejecutar la consulta
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
        private int ObtenerIDUsuarioPordni(string dni)
        {
            int idUsuario = 0;
            conectar = new ConexionBD();

            try
            {
                conectar.AbrirConexion();

                // Consulta SQL para obtener el IDUsuario
                string consulta = "SELECT IDUsuario FROM usuarios WHERE DNI = @DNI LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    // Asignar el valor del parámetro
                    cmd.Parameters.AddWithValue("@DNI", dni);

                    // Ejecutar la consulta y leer el resultado
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idUsuario = reader.GetInt32("IDUsuario");
                        }
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

            return idUsuario;
        }





    }
}
