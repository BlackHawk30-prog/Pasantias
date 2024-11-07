using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;

namespace Pasantias
{
    public partial class Editar_Perfil : Page
    {
        // Método para cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay un IDUsuario en la sesión
                if (Session["UserID"] != null)
                {
                    int idUsuario = (int)Session["UserID"];

                    // Log de depuración para confirmar el IDUsuario recibido
                    System.Diagnostics.Debug.WriteLine($"IDUsuario recibido de la sesión: {idUsuario}");

                    // Cargar los datos del usuario
                    CargarDatosUsuario(idUsuario);
                }
                else
                {
                    // Log de depuración indicando que no se encontró el IDUsuario en la sesión
                    System.Diagnostics.Debug.WriteLine("No se encontró el IDUsuario en la sesión.");
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // Método para cargar los datos del usuario desde la base de datos
        private void CargarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                // Consulta actualizada con los nombres correctos de las columnas
                string consulta = @"
                    SELECT 
                        u.IDUsuario,
                        u.Primer_Nombre, 
                        u.Segundo_Nombre, 
                        u.Primer_Apellido, 
                        u.Segundo_Apellido, 
                        u.DNI, 
                        u.Correo, 
                        u.Usuario, 
                        u.Password, 
                        du.Telefono, 
                        du.Direccion, 
                        du.Grado_academico, 
                        du.Sexo, 
                        du.Foto, 
                        du.Curriculum 
                    FROM 
                        usuarios u 
                    JOIN 
                        datos_usuario du 
                    ON 
                        u.IDUsuario = du.IDUsuario 
                    WHERE 
                        u.IDUsuario = @userId";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("Primer Nombre: " + reader["Primer_Nombre"].ToString());
                            System.Diagnostics.Debug.WriteLine("Segundo Nombre: " + reader["Segundo_Nombre"].ToString());
                            System.Diagnostics.Debug.WriteLine("Primer Apellido: " + reader["Primer_Apellido"].ToString());
                            // Continúa para cada campo...

                            // Log de depuración indicando que se encontraron datos
                            System.Diagnostics.Debug.WriteLine("Datos cargados para el usuario.");

                            // Llenar los controles con los datos obtenidos
                            txt_Nombre1.Text = reader["Primer_Nombre"].ToString();
                            txt_Nombre2.Text = reader["Segundo_Nombre"].ToString();
                            txt_Apellido1.Text = reader["Primer_Apellido"].ToString();
                            txt_Apellido2.Text = reader["Segundo_Apellido"].ToString();
                            txt_DNI.Text = reader["DNI"].ToString();
                            txt_Correo.Text = reader["Correo"].ToString();
                            txt_Fecha.Text = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("yyyy-MM-dd");
                            txt_Telefono.Text = reader["Telefono"].ToString();
                            txt_Universidad.Text = reader["Grado_academico"].ToString(); // Ajusta si es necesario
                            txt_Direccion.Text = reader["Direccion"].ToString();

                            // Seleccionar el sexo adecuado en el RadioButton
                            if (reader["Sexo"].ToString() == "Hombre")
                            {
                                txt_Hombre.Checked = true;
                            }
                            else if (reader["Sexo"].ToString() == "Mujer")
                            {
                                txt_Mujer.Checked = true;
                            }
                        }
                        else
                        {
                            // Log de depuración indicando que no se encontraron datos
                            System.Diagnostics.Debug.WriteLine("No se encontraron datos para el usuario.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log de depuración en caso de error
                System.Diagnostics.Debug.WriteLine("Error al cargar los datos del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }
    }
}
