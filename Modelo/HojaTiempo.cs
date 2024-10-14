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

        public int crear(int IDHoja, string Fecha, string Actividades, int Horas, int IDUsuario)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("Insert into Hoja_Tiempo(IDHoja,Fecha,Actividades,Horas,IDUsuario) Values ({0}, '{1}','{2}', {3}, {4})", IDHoja,Fecha,Actividades,Horas,IDUsuario );
            MySqlCommand query = new MySqlCommand(consulta,conectar.conectar);
            query.Connection = conectar.conectar;
            no = query.ExecuteNonQuery();

            conectar.CerarConexion();
            return no;

        }
        public int actualizar(int IDHoja, string Fecha, string Actividades, int Horas, int IDUsuario)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = string.Format("UPDATE Hoja_Tiempo SET Fecha='{1}', Actividades='{2}', Horas={3}, IDUsuario={4} WHERE IDHoja={0};", IDHoja, Fecha, Actividades, Horas, IDUsuario);
            MySqlCommand query = new MySqlCommand(consulta, conectar.conectar);
            query.Connection = conectar.conectar;
            no = query.ExecuteNonQuery();

            conectar.CerarConexion();
            return no;

        }
    }
}
