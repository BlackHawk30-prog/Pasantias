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
    public class Finanza
    {
        ConexionBD conectar;

        // Método privado que devuelve una tabla con los datos de Finanzas
        private DataTable grid_fina(string condicionAdicional = "")
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            int idUsuario = SessionStore.UserID;

            // Consulta SQL con JOIN y parámetro
            string consulta = "SELECT ht.IDHojaTiempo, u.Primer_Nombre, u.DNI, u.* " +
                              "FROM usuarios u " +
                              "JOIN hoja_tiempo ht ON u.IDUsuario = ht.IDUsuario " +
                      "WHERE  ht.FConfirmado = 0 " +
                      "  AND ht.RHConfirmado = 1 " +
                      "  AND ht.oConfirmado = 1 " +
                      "  AND ht.PConfirmado = 1 ";

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

        // Método público que vincula el GridView con los datos obtenidos
        public void grid_fina(GridView grid, string condicionrecursos)
        {
            grid.DataSource = grid_fina();
            grid.DataBind();
        }

        public void AceptarHojaDeTiempo(int idHojaTiempo)
        {
            // Establece la conexión con la base de datos
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Actualiza solo la hoja de tiempo seleccionada
            string consulta = "UPDATE hoja_tiempo SET FConfirmado = 1 WHERE IDHojaTiempo = @IDHojaTiempo";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDHojaTiempo", idHojaTiempo);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }

        public void RechazarHojaDeTiempo(int idHojaTiempo)
        {
            // Establece la conexión con la base de datos
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Actualiza solo la hoja de tiempo seleccionada
            string consulta = "UPDATE hoja_tiempo SET RHConfirmado = 0 WHERE IDHojaTiempo = @IDHojaTiempo";
            MySqlCommand comando = new MySqlCommand(consulta, conectar.conectar);
            comando.Parameters.AddWithValue("@IDHojaTiempo", idHojaTiempo);
            comando.ExecuteNonQuery();

            conectar.CerrarConexion();
        }
    }
}
