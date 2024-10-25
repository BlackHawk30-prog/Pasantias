using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            int no = 0;  // Esta variable almacenará el número de filas afectadas
            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "CrearDatosUsuario";  // Nombre del procedimiento almacenado

            // Crear el comando y configurarlo como procedimiento almacenado
            using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Indicar que es un procedimiento almacenado

                // Agregar los parámetros al comando
                cmd.Parameters.AddWithValue("p_Telefono", Telefono);
                cmd.Parameters.AddWithValue("p_Direccion", Direccion);
                cmd.Parameters.AddWithValue("p_Grado_Academico", Grado_Academico);
                cmd.Parameters.AddWithValue("p_Sexo", sexo);
                cmd.Parameters.AddWithValue("p_Foto", Foto);  // Asignar los bytes de la foto
                cmd.Parameters.AddWithValue("p_Curriculum", Curriculum);  // Asignar los bytes del currículum
                cmd.Parameters.AddWithValue("p_IDUsuario", IDUsuario);

                // Ejecutar el procedimiento almacenado
                no = cmd.ExecuteNonQuery();  // Devuelve el número de filas afectadas
            }

            conectar.CerrarConexion();
            return no;  // Devolver el número de filas afectadas
        }

    }

}
