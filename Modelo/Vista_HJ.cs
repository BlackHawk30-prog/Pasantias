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
    public class Vista_HJ
    {
        ConexionBD conectar;

        public DataTable grid_hojas()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            int mesActual = DateTime.Now.Month;
            int anioActual = DateTime.Now.Year;

            string consulta = $"SELECT * FROM hoja_tiempo WHERE Eliminado = 0 AND MONTH(fecha) = {mesActual} AND YEAR(fecha) = {anioActual}";

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
    }
}
