using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Modelo
{
    public class HojaTiempo
    {
        ConexionBD conectar;

        // Método para obtener las hojas de tiempo
        public DataTable grid_hojas()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Obtener el ID de Hoja de Tiempo desde la sesión
            int idHojaTiempo = SessionStore.HojaID;

            string consulta = $"SELECT * FROM detalle_hoja_tiempo WHERE IDHojaTiempo = {idHojaTiempo}";

            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();
            return tabla;
        }

        // Método para llenar el GridView con los datos de las hojas de tiempo
        public void grid_hojas(GridView grid)
        {
            grid.DataSource = grid_hojas();
            grid.DataBind();
        }

        // Método para crear una nueva hoja de tiempo
        public int crear(string Fecha, string Actividades, int Horas, int IDHojaTiempo)
        {
            int no = 0;  // Esta variable almacenará el número de filas afectadas
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "INSERT INTO detalle_hoja_tiempo (Fecha, Actividades, Horas, IDHojaTiempo) " +
                              "VALUES (@Fecha, @Actividades, @Horas, @IDHojaTiempo)";

            // Crear el comando SQL
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                // Agregar los parámetros al comando
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Actividades", Actividades);
                cmd.Parameters.AddWithValue("@Horas", Horas);
                cmd.Parameters.AddWithValue("@IDHojaTiempo", IDHojaTiempo);

                // Ejecutar el comando SQL
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        // Método para actualizar una hoja de tiempo existente
        public int actualizar(int IDHoja, string Fecha, string Actividades, int Horas, int IDHojaTiempo)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE detalle_hoja_tiempo SET Fecha = @Fecha, Actividades = @Actividades, " +
                              "Horas = @Horas, IDHojaTiempo = @IDHojaTiempo WHERE ID_Detalle = @IDHoja";

            // Crear el comando SQL
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                // Agregar los parámetros al comando
                cmd.Parameters.AddWithValue("@IDHoja", IDHoja);
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Actividades", Actividades);
                cmd.Parameters.AddWithValue("@Horas", Horas);
                cmd.Parameters.AddWithValue("@IDHojaTiempo", IDHojaTiempo);

                // Ejecutar el comando SQL
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        // Método para eliminar una hoja de tiempo
        public int eliminar(int IDHoja)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "DELETE FROM detalle_hoja_tiempo WHERE ID_Detalle = @IDHoja";

            // Crear el comando SQL
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                // Agregar el parámetro al comando
                cmd.Parameters.AddWithValue("@IDHoja", IDHoja);

                // Ejecutar el comando SQL
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        public int ConfirmarHojaTiempo(int IDHojaTiempo)
        {
            int filasAfectadas = 0; // Variable para almacenar el número de filas afectadas
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                // Consulta para actualizar el campo PConfirmado a 1 basado en el IDHojaTiempo
                string consulta = "UPDATE hoja_tiempo SET PConfirmado = 1 WHERE IDHojaTiempo = @IDHojaTiempo";

                // Crear el comando SQL
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    // Agregar el parámetro para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@IDHojaTiempo", IDHojaTiempo);

                    // Ejecutar el comando y obtener el número de filas afectadas
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra
                Console.WriteLine("Error al confirmar la hoja de tiempo: " + ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar la conexión a la base de datos
                conectar.CerrarConexion();
            }

            return filasAfectadas; // Retornar el número de filas afectadas
        }
        public int ObtenerPConfirmado(int idHojaTiempo)
        {
            int pConfirmado = 0; // Inicializar la variable de estado
            conectar = new ConexionBD(); // Crear instancia de conexión
            conectar.AbrirConexion(); // Abrir conexión a la base de datos

            try
            {
                // Consulta para obtener el estado de PConfirmado basado en el IDHojaTiempo
                string query = "SELECT PConfirmado FROM hoja_tiempo WHERE IDHojaTiempo = @IDHojaTiempo";

                // Crear el comando SQL
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    // Agregar parámetro al comando para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@IDHojaTiempo", idHojaTiempo);

                    // Ejecutar la consulta y leer el valor de PConfirmado
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        pConfirmado = Convert.ToInt32(resultado); // Convertir el resultado a entero
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
                Console.WriteLine("Error al obtener el estado de PConfirmado: " + ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar la conexión a la base de datos
                conectar.CerrarConexion();
            }

            return pConfirmado; // Retornar el estado de PConfirmado
        }


    }
}
