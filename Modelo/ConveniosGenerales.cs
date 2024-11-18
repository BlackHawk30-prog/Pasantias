using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web;

namespace Modelo
{
    public class ConveniosGenerales
    {
        ConexionBD conectar;
        private DataTable grid_Convenios()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("SELECT * FROM convenio");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();
            return tabla;
         
        }


        public void grid_Convenios(GridView grid)
        {
            grid.DataSource = grid_Convenios();
            grid.DataBind();
        }
    }
}
