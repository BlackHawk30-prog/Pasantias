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
            string consulta = "SELECT * FROM hoja_tiempo WHERE Eliminado = 0";
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerarConexion();
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

            conectar.CerarConexion();
            return no;  // Devolver el número de filas afectadas
        }

        public int actualizar(int IDHoja, string Fecha, string Actividades, int Horas, int IDUsuario)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("UPDATE Hoja_Tiempo SET Fecha='{1}', Actividades='{2}', Horas={3}, IDUsuario={4} WHERE IDHoja={0};", IDHoja, Fecha, Actividades, Horas, IDUsuario);
            MySqlCommand query = new MySqlCommand(consulta, conectar.conectar);
            query.Connection = conectar.conectar;
            no = query.ExecuteNonQuery();

            conectar.CerarConexion();
            return no;

        }
        public int eliminar(int IDHoja)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("UPDATE Hoja_Tiempo SET eliminado = 1 WHERE IDHoja = {0};", IDHoja);
            MySqlCommand query = new MySqlCommand(consulta, conectar.conectar);
            query.Connection = conectar.conectar;
            no = query.ExecuteNonQuery();

            conectar.CerarConexion();
            return no;

        }
    }
}
