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
            string consulta = string.Format("Select * from hoja_tiempo");
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

    }
}
