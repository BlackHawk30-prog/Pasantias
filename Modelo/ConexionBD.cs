using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modelo
{
    public class ConexionBD : IDisposable
    {
        private string cadena = "server=localhost; database=pasantias; user=root; password=";
        public MySqlConnection conectar;

        public ConexionBD()
        {
            conectar = new MySqlConnection(cadena);
        }

        public void AbrirConexion()
        {
            try
            {
                if (conectar.State == ConnectionState.Closed)
                {
                    conectar.Open();
                    // System.Diagnostics.Debug.WriteLine("Conexion Exitosa");
                }
            }
            catch (Exception ex)
            {
                // System.Diagnostics.Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conectar.State == ConnectionState.Open)
                {
                    conectar.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Implementación de IDisposable
        public void Dispose()
        {
            // Cierra la conexión si está abierta
            CerrarConexion();
            conectar.Dispose(); // Libera los recursos de la conexión
        }
    }
}
