using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace Modelo
{
    public class Oficial
    {
        ConexionBD conectar;
       

        private DataTable drop_puesto()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("select IDUsuario as id, Primer_Nombre, DNI from usuarios;");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        private DataTable grid_oficial(string condicionAdicional = "")
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            int idUsuario = SessionStore.UserID;

            // Consulta SQL con JOIN y parámetro
            string consulta = "SELECT u.* " +
                              "FROM usuarios u " +
                              "JOIN hoja_tiempo ht ON u.IDUsuario = ht.IDUsuario " +
                              "WHERE u.supervisor = @idUsuario AND ht.oConfirmado = 0";

            // Agregar condiciones adicionales si es necesario
            if (!string.IsNullOrWhiteSpace(condicionAdicional))
            {
                consulta += " AND " + condicionAdicional;
            }

            using (MySqlCommand command = new MySqlCommand(consulta, conectar.conectar))
            {
                // Agregar el parámetro a la consulta
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                // Usar MySqlDataAdapter para llenar el DataTable
                MySqlDataAdapter query = new MySqlDataAdapter(command);
                query.Fill(tabla);
            }

            conectar.CerrarConexion();

            return tabla;
        }





        public void grid_oficial(GridView grid, string condicionrecursos)
        {
            grid.DataSource = grid_oficial();
            grid.DataBind();
        }



        public void AceptarOficial(int idUsuario)
        {
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "UPDATE hoja_tiempo SET OConfirmado = 1 WHERE IDUsuario = @IDUsuario";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }
    }
}
