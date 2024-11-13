using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;

namespace Pasantias
{
    public partial class Editar_Perfil : Page
    {
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

                    // Asignar la URL de la imagen y el enlace del curriculum si existen en la base de datos
                    string imagePath = ObtenerRutaFotoDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        imgFoto.ImageUrl = ResolveUrl("~/" + imagePath);  // Asegurarse de que la URL esté correctamente formateada
                    }

                    string curriculumPath = ObtenerRutaCurriculumDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(curriculumPath))
                    {
                        lnkCurriculum.NavigateUrl = ResolveUrl("~/" + curriculumPath);  // Asegurar la URL correcta
                        lnkCurriculum.Visible = true;
                    }
                }
                else
                {
                    // Log de depuración indicando que no se encontró el IDUsuario en la sesión
                    System.Diagnostics.Debug.WriteLine("No se encontró el IDUsuario en la sesión.");
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
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
                        du.Fecha_Nacimiento,
                        du.Telefono, 
                        du.Direccion, 
                        du.Grado_academico, 
                        du.Sexo
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
                            txt_Nombre1.Text = reader["Primer_Nombre"].ToString();
                            txt_Nombre2.Text = reader["Segundo_Nombre"].ToString();
                            txt_Apellido1.Text = reader["Primer_Apellido"].ToString();
                            txt_Apellido2.Text = reader["Segundo_Apellido"].ToString();
                            txt_DNI.Text = reader["DNI"].ToString();
                            txt_Correo.Text = reader["Correo"].ToString();
                            txt_Fecha.Text = Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                            txt_Telefono.Text = reader["Telefono"].ToString();
                            txt_Universidad.Text = reader["Grado_academico"].ToString();
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
                            System.Diagnostics.Debug.WriteLine("No se encontraron datos para el usuario.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar los datos del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }

        // Método para obtener la ruta de la foto desde la base de datos
        private string ObtenerRutaFotoDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaFoto = null;

            try
            {
                string consulta = "SELECT Foto FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaFoto = resultado.ToString();  // Almacena la ruta como texto
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener la foto del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaFoto;
        }

        // Método para obtener la ruta del curriculum desde la base de datos
        private string ObtenerRutaCurriculumDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaCurriculum = null;

            try
            {
                string consulta = "SELECT Curriculum FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaCurriculum = resultado.ToString();  // Almacena la ruta como texto
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener el curriculum del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaCurriculum;
        }
    }
}
