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
    public class Postulacion
    {
        ConexionBD conectar;

        private DataTable grid_aplicantes()
        {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string consulta = ("SELECT u.IDUsuario, u.Primer_Nombre, u.Segundo_Nombre, u.Primer_Apellido, " +
                  "u.Segundo_Apellido, u.DNI, u.Correo, u.Usuario, u.Password, " +
                  "du.IDDatos_Usuarios, du.Fecha_Nacimiento, du.Telefono, du.Direccion, " +
                  "du.Grado_academico, du.Sexo, du.Foto, du.Curriculum, r.IDRol " +
                  "FROM usuarios u " +
                  "JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario " +
                  "JOIN roles_usuarios ru ON u.IDUsuario = ru.IDUsuario " +
                  "JOIN roles r ON ru.IDRol = r.IDRol " +
                  "WHERE r.IDRol = 1;");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerrarConexion();

            return tabla;
        }

        public void grid_aplicantes(GridView grid)
        {
            grid.DataSource = grid_aplicantes();
            grid.DataBind();
        }
    }
}
