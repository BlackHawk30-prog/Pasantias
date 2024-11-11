using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Modelo
{
    public class HojasGenerales
    {
        ConexionBD conectar;
        private DataTable grid_Generales()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("Select * From hoja_tiempo;");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        public void grid_Generales(GridView grid)
        {
            grid.DataSource = grid_Generales();
            grid.DataBind();
        }

    }
}
