using MySql.Data.MySqlClient;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data;
using Mysqlx.Cursor;
using Org.BouncyCastle.Utilities;

namespace Modelo
{
    public class Aplicante1
    {
        ConexionBD conectar;

        // Método para agregar un nuevo aplicante
        public int agregar(int IDUsuario, string Nombre1, string Nombre2, string Apellido1, string Apellido2, string DNI, string Correo)
        {
            // Primero, verifica si el DNI ya existe
            if (ExisteDNI(DNI))
            {
                return -1; // Retorna -1 para indicar que el DNI ya existe
            }

            int no = 0;
            conectar = new ConexionBD();

            conectar.AbrirConexion();
            string consulta = string.Format("Insert into usuarios (IDUsuario, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, DNI, Correo) Values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", IDUsuario, Nombre1, Nombre2, Apellido1, Apellido2, DNI, Correo);
            MySqlCommand query = new MySqlCommand(consulta, conectar.conectar);
            no = query.ExecuteNonQuery();

            conectar.CerrarConexion();
            return no;
        }

        // Método para verificar si el DNI ya existe
        public bool ExisteDNI(string dni)
        {
            int count = 0;
            using (var conectar = new ConexionBD())
            {
                conectar.AbrirConexion();
                string consulta = "SELECT COUNT(*) FROM usuarios WHERE DNI = @DNI"; // Cambia 'usuarios' al nombre de tu tabla real

                using (var cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count > 0; // Retorna true si el DNI existe
        }
    }

}
