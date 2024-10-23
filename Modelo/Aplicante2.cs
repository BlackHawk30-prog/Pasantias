using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Aplicante2
    {
        ConexionBD conectar;

        public int crear(int Telefono, string Direccion, string Grado_Academico, string sexo, byte[] Foto, byte[] Curriculum, int IDUsuario)
        {
            int no = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            // Consulta sin el campo autoincrementable IDDatos_Usuario
            string consulta = "INSERT INTO datos_usuario (Telefono, Direccion, Grado_Academico, sexo, Foto, Curriculum, IDUsuario) " +
                              "VALUES (@Telefono, @Direccion, @Grado_Academico, @Sexo, @Foto, @Curriculum, @IDUsuario)";

            MySqlCommand query = new MySqlCommand(consulta, conectar.conectar);
            query.Parameters.AddWithValue("@Telefono", Telefono);
            query.Parameters.AddWithValue("@Direccion", Direccion);
            query.Parameters.AddWithValue("@Grado_Academico", Grado_Academico);
            query.Parameters.AddWithValue("@Sexo", sexo);
            query.Parameters.AddWithValue("@Foto", Foto); // Asignar los bytes de la foto
            query.Parameters.AddWithValue("@Curriculum", Curriculum); // Asignar los bytes del currículum
            query.Parameters.AddWithValue("@IDUsuario", IDUsuario);

            no = query.ExecuteNonQuery();
            conectar.CerrarConexion();

            return no;
        }

    }

}
