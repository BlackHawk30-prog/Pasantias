using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class Finanza
    {
        ConexionBD conectar;

        // Método privado que devuelve una tabla con los datos de Finanzas
        private DataTable grid_fina()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = "select IDUsuario as id, Primer_Nombre, Segundo_Nombre, DNI from usuarios;";
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        // Método público que vincula el GridView con los datos obtenidos
        public void grid_fina(GridView grid)
        {
            grid.DataSource = grid_fina();
            grid.DataBind();
        }
    }
}
