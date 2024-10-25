using MySql.Data.MySqlClient;
using System;

namespace Modelo
{
    public class Aplicante1
    {
        ConexionBD conectar;

        // Método para agregar un nuevo aplicante
        public int agregar(int IDUsuario,string Nombre1, string Nombre2, string Apellido1, string Apellido2, string DNI, string Correo)
        {
            // Primero, verifica si el DNI ya existe
            if (ExisteDNI(DNI))
            {
                return -1; // Retorna -1 para indicar que el DNI ya existe
            }

            conectar = new ConexionBD();
            conectar.AbrirConexion();

            string consulta = "INSERT INTO usuarios (IDUsuario,Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, DNI, Correo) " +
                              "VALUES (@IDUsuario, @Nombre1, @Nombre2, @Apellido1, @Apellido2, @DNI, @Correo)";

            using (MySqlCommand query = new MySqlCommand(consulta, conectar.conectar))
            {
                query.Parameters.AddWithValue("@IDUsuario", IDUsuario);
                query.Parameters.AddWithValue("@Nombre1", Nombre1);
                query.Parameters.AddWithValue("@Nombre2", Nombre2);
                query.Parameters.AddWithValue("@Apellido1", Apellido1);
                query.Parameters.AddWithValue("@Apellido2", Apellido2);
                query.Parameters.AddWithValue("@DNI", DNI);
                query.Parameters.AddWithValue("@Correo", Correo);

                query.ExecuteNonQuery();
            }

            conectar.CerrarConexion();
            return ObtenerUltimoIDUsuario(); // Retorna el ID del nuevo registro
        }

        public int ObtenerUltimoIDUsuario()
        {
            int idUsuario = 0;
            using (var conectar = new ConexionBD())
            {
                conectar.AbrirConexion();
                string consulta = "SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return idUsuario;
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
