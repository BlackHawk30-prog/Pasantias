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

            int idHojaTiempo = SessionStore.HojaID;

            using (MySqlCommand cmd = new MySqlCommand("GetDetalleHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", idHojaTiempo);

                using (MySqlDataAdapter query = new MySqlDataAdapter(cmd))
                {
                    query.Fill(tabla);
                }
            }

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
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand("InsertDetalleHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Fecha", Fecha);
                cmd.Parameters.AddWithValue("p_Actividades", Actividades);
                cmd.Parameters.AddWithValue("p_Horas", Horas);
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", IDHojaTiempo);

                no = cmd.ExecuteNonQuery();
            }

            conectar.CerrarConexion();
            return no;
        }


        // Método para actualizar una hoja de tiempo existente
        public int actualizar(int IDHoja, string Fecha, string Actividades, int Horas, int IDHojaTiempo)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand("UpdateDetalleHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHoja", IDHoja);
                cmd.Parameters.AddWithValue("p_Fecha", Fecha);
                cmd.Parameters.AddWithValue("p_Actividades", Actividades);
                cmd.Parameters.AddWithValue("p_Horas", Horas);
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", IDHojaTiempo);

                no = cmd.ExecuteNonQuery();
            }

            conectar.CerrarConexion();
            return no;
        }


        // Método para eliminar una hoja de tiempo
        public int eliminar(int IDHoja)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand("DeleteDetalleHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHoja", IDHoja);

                no = cmd.ExecuteNonQuery();
            }

            conectar.CerrarConexion();
            return no;
        }


        public int ConfirmarHojaTiempo(int IDHojaTiempo)
        {
            int filasAfectadas = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand("ConfirmHojaTiempo", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", IDHojaTiempo);

                filasAfectadas = cmd.ExecuteNonQuery();
            }

            conectar.CerrarConexion();
            return filasAfectadas;
        }

        public int ObtenerPConfirmado(int idHojaTiempo)
        {
            int pConfirmado = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand("GetPConfirmado", conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_IDHojaTiempo", idHojaTiempo);

                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                {
                    pConfirmado = Convert.ToInt32(resultado);
                }
            }

            conectar.CerrarConexion();
            return pConfirmado;
        }



    }
}
