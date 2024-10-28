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
    public class Finanzas
    {
        ConexionBD conectar;

        private DataTable grid_fina()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("select IDUsuario as id, Primer_Nombre, Segundo_Nombre, DNI from usuarios;");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        public void grid_fina(GridView grid)
        {
            grid.DataSource = grid_fina();
            grid.DataBind();
        }
    }
}
