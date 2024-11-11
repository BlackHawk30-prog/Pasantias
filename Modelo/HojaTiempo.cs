using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;
using Org.BouncyCastle.Asn1.Mozilla;

namespace Modelo
{
    public class HojaTiempo
    {
        ConexionBD conectar;
        public DataTable grid_hojas()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            int mesActual = DateTime.Now.Month;
            int anioActual = DateTime.Now.Year;

            // Obtener el ID del usuario desde la sesión
            int idUsuario = SessionStore.UserID;
            //string consulta = $"SELECT * FROM hoja_tiempo WHERE Eliminado = 0 AND MONTH(fecha) = {mesActual} AND YEAR(fecha) = {anioActual} AND idUsuario = {idUsuario}";
            string consulta = $"SELECT * FROM detalle_hoja_tiempo WHERE MONTH(fecha) = {mesActual} AND YEAR(fecha) = {anioActual} AND IDHojaTiempo = 1";

            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();
            return tabla;
        }


        public void grid_hojas(GridView grid)
        {
            grid.DataSource = grid_hojas();
            grid.DataBind();
        }

        public int crear(int IDHoja, string Fecha, string Actividades, int Horas, int IDUsuario)
        {
            int no = 0;  // Esta variable almacenará el número de filas afectadas
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "InsertarHojaTiempo";  // Nombre del procedimiento almacenado


            // Crear el comando y configurarlo como procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Indicar que es un procedimiento almacenado

                // Agregar los parámetros al comando
                cmd.Parameters.AddWithValue("p_IDHoja", IDHoja);
                cmd.Parameters.AddWithValue("p_Fecha", Fecha);
                cmd.Parameters.AddWithValue("p_Actividades", Actividades);
                cmd.Parameters.AddWithValue("p_Horas", Horas);
                cmd.Parameters.AddWithValue("p_IDUsuario", IDUsuario);

                // Ejecutar el procedimiento almacenado
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        public int actualizar(int IDHoja, string Fecha, string Actividades, int Horas, int IDUsuario)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "ActualizarHojaTiempo";  // Nombre del procedimiento almacenado

            // Crear el comando y configurarlo como procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Indicar que es un procedimiento almacenado

                // Agregar los parámetros al comando
                cmd.Parameters.AddWithValue("p_IDHoja", IDHoja);
                cmd.Parameters.AddWithValue("p_Fecha", Fecha);
                cmd.Parameters.AddWithValue("p_Actividades", Actividades);
                cmd.Parameters.AddWithValue("p_Horas", Horas);
                cmd.Parameters.AddWithValue("p_IDUsuario", IDUsuario);

                // Ejecutar el procedimiento almacenado
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        public int eliminar(int IDHoja)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "EliminarHojaTiempo";  // Nombre del procedimiento almacenado

            // Crear el comando y configurarlo como procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Indicar que es un procedimiento almacenado

                // Agregar el parámetro al comando
                cmd.Parameters.AddWithValue("p_IDHoja", IDHoja);

                // Ejecutar el procedimiento almacenado
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

    }
}
