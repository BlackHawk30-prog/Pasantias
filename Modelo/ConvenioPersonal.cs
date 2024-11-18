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
    public class ConvenioPersonal
    {
        ConexionBD conectar;
        private DataTable grid_Convenio_personal()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("SELECT * FROM convenio Where IDUsuario = 94");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();
            return tabla;

        }


        public void grid_Convenio_personal(GridView grid)
        {
            grid.DataSource = grid_Convenio_personal();
            grid.DataBind();
        }
    }
}
